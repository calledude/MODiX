using Discord.WebSocket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modix.Models.Core;
using Modix.Web.Shared.Models.Promotions;
using Modix.Web.Shared.Services;

namespace Modix.Web.Controllers;

[Route("~/api/campaigns")]
[ApiController]
[Authorize(Roles = nameof(AuthorizationClaim.PromotionsRead))]
public class CampaignController : ModixController
{
    private readonly ICampaignService _campaignService;

    public CampaignController(ICampaignService campaignService, DiscordSocketClient discordSocketClient, Modix.Services.Core.IAuthorizationService authorizationService)
        : base(discordSocketClient, authorizationService)
    {
        _campaignService = campaignService;
    }

    [HttpGet]
    public async IAsyncEnumerable<PromotionCampaignData> GetCampaignsAsync()
    {
        var campaigns = await _campaignService.GetCampaignsAsync();

        foreach (var campaign in campaigns)
        {
            yield return campaign;
        }
    }

    [HttpGet("{campaignId}")]
    public async Task<IActionResult> GetCampaignCommentsAsync(long campaignId)
    {
        var campaignComments = await _campaignService.GetCampaignCommentsAsync(campaignId);

        if (campaignComments is null)
            return NotFound();

        return Ok(campaignComments);
    }

    [HttpPut("{campaignId}/createcomment")]
    public async Task<CampaignCommentData> CreateCommentAsync(long campaignId, [FromBody] CampaignCommentData campaignCommentData)
    {
        return await _campaignService.CreateCommentAsync(campaignId, campaignCommentData);
    }

    [HttpPatch("updatecomment")]
    public async Task<CampaignCommentData> UpdateCommentAsync([FromBody] CampaignCommentData campaignCommentData)
    {
        return await _campaignService.UpdateCommentAsync(campaignCommentData);
    }

    [HttpPost("{campaignId}/accept/{force}")]
    [Authorize(Roles = nameof(AuthorizationClaim.PromotionsCloseCampaign))]
    public async Task AcceptCampaignAsync(long campaignId, bool force)
    {
        await _campaignService.AcceptCampaignAsync(campaignId, force);
    }

    [HttpPost("{campaignId}/reject")]
    [Authorize(Roles = nameof(AuthorizationClaim.PromotionsCloseCampaign))]
    public async Task RejectCampaignAsync(long campaignId)
    {
        await _campaignService.RejectCampaignAsync(campaignId);
    }

    [HttpGet("{subjectId}/nextrank")]
    [Authorize(Roles = nameof(AuthorizationClaim.PromotionsCreateCampaign))]
    public async Task<NextRank> GetNextRankRoleForUserAsync(ulong subjectId)
    {
        return await _campaignService.GetNextRankRoleForUserAsync(subjectId);
    }

    [HttpPut("create")]
    [Authorize(Roles = nameof(AuthorizationClaim.PromotionsCreateCampaign))]
    public async Task CreateAsync([FromBody] PromotionCreationData creationData)
    {
        await _campaignService.CreateAsync(creationData);
    }
}
