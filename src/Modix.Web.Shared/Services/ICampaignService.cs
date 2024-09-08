using Modix.Web.Shared.Models.Promotions;

namespace Modix.Web.Shared.Services;

public interface ICampaignService
{
    Task<IEnumerable<PromotionCampaignData>> GetCampaignsAsync();
    Task<Dictionary<long, CampaignCommentData>?> GetCampaignCommentsAsync(long campaignId);
    Task<CampaignCommentData> CreateCommentAsync(long campaignId, CampaignCommentData campaignCommentData);
    Task<CampaignCommentData> UpdateCommentAsync(CampaignCommentData campaignCommentData);
    Task AcceptCampaignAsync(long campaignId, bool force);
    Task RejectCampaignAsync(long campaignId);
    Task<NextRank> GetNextRankRoleForUserAsync(ulong subjectId);
    Task CreateAsync(PromotionCreationData creationData);
}
