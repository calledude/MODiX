using Modix.Data.Models.Promotions;
using Modix.Services.Promotions;
using Modix.Services.Utilities;
using Modix.Web.Shared.Models.Promotions;
using Modix.Web.Shared.Services;

namespace Modix.Web.Services;

public sealed class CampaignService : ICampaignService
{
    private readonly IPromotionsService _promotionsService;
    private readonly UserHelper _userHelper;

    public CampaignService(IPromotionsService promotionsService, UserHelper userHelper)
    {
        _promotionsService = promotionsService;
        _userHelper = userHelper;
    }

    public async Task<IEnumerable<PromotionCampaignData>> GetCampaignsAsync()
    {
        var currentUser = await _userHelper.GetAuthenticatedUserAsync();

        var campaigns = await _promotionsService.SearchCampaignsAsync(new PromotionCampaignSearchCriteria
        {
            GuildId = currentUser.Guild.Id
        });

        return campaigns.Select(campaign =>
        {
            return new PromotionCampaignData
            {
                Id = campaign.Id,
                SubjectId = campaign.Subject.Id,
                SubjectName = campaign.Subject.GetFullUsername(),
                TargetRoleId = campaign.TargetRole.Id,
                TargetRoleName = campaign.TargetRole.Name,
                Outcome = campaign.Outcome,
                Created = campaign.CreateAction.Created,
                IsCurrentUserCampaign = campaign.Subject.Id == currentUser.Id,
                ApproveCount = campaign.ApproveCount,
                OpposeCount = campaign.OpposeCount,
                IsClosed = campaign.CloseAction is not null
            };
        });
    }

    public async Task<Dictionary<long, CampaignCommentData>?> GetCampaignCommentsAsync(long campaignId)
    {
        var campaignDetails = await _promotionsService.GetCampaignDetailsAsync(campaignId);

        if (campaignDetails is null)
            return null;

        var currentUser = await _userHelper.GetAuthenticatedUserAsync();

        return campaignDetails.Comments
            .Where(x => x.ModifyAction is null)
            .Select(x => new CampaignCommentData
                (
                    x.Id,
                    x.Sentiment,
                    x.Content,
                    x.CreateAction.Created,
                    x.CreateAction.CreatedBy.Id == currentUser.Id
                )
            )
            .ToDictionary(x => x.Id);
    }

    public async Task<CampaignCommentData> CreateCommentAsync(long campaignId, CampaignCommentData campaignCommentData)
    {
        var promotionActionSummary = await _promotionsService.AddCommentAsync(
            campaignId,
            campaignCommentData.PromotionSentiment,
            campaignCommentData.Content);

        var newComment = promotionActionSummary.NewComment;

        return new CampaignCommentData(newComment.Id, newComment.Sentiment, newComment.Content, promotionActionSummary.Created, true);
    }

    public async Task<CampaignCommentData> UpdateCommentAsync(CampaignCommentData campaignCommentData)
    {
        var promotionActionSummary = await _promotionsService.UpdateCommentAsync(
            campaignCommentData.Id,
            campaignCommentData.PromotionSentiment,
            campaignCommentData.Content);

        var newComment = promotionActionSummary.NewComment;

        return new CampaignCommentData(newComment.Id, newComment.Sentiment, newComment.Content, promotionActionSummary.Created, true);
    }

    public async Task AcceptCampaignAsync(long campaignId, bool force)
    {
        await _promotionsService.AcceptCampaignAsync(campaignId, force);
    }

    public async Task RejectCampaignAsync(long campaignId)
    {
        await _promotionsService.RejectCampaignAsync(campaignId);
    }

    public async Task<NextRank> GetNextRankRoleForUserAsync(ulong subjectId)
    {
        var nextRank = await _promotionsService.GetNextRankRoleForUserAsync(subjectId);

        if (nextRank is null)
            return new NextRank("None", "#607d8b");

        var currentUser = await _userHelper.GetAuthenticatedUserAsync();
        var color = currentUser.Guild.Roles.First(r => r.Id == nextRank.Id).Color;
        return new NextRank(nextRank.Name, color.ToString());
    }

    public async Task CreateAsync(PromotionCreationData creationData)
    {
        await _promotionsService.CreateCampaignAsync(creationData.UserId, creationData.Comment);
    }
}
