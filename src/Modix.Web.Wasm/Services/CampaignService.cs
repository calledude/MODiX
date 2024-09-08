using Modix.Web.Shared.Models.Common;
using System.Net.Http.Json;
using System.Net.Http;
using Modix.Web.Shared.Models.Promotions;
using Modix.Web.Shared.Services;
using System.ComponentModel.Design;

namespace Modix.Web.Wasm.Services;

public sealed class CampaignService : ICampaignService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CampaignService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<PromotionCampaignData>> GetCampaignsAsync()
    {
        using var client = _httpClientFactory.CreateClient("api");
        return await client.GetFromJsonAsync<PromotionCampaignData[]>("api/campaigns");
    }

    public async Task<Dictionary<long, CampaignCommentData>?> GetCampaignCommentsAsync(long campaignId)
    {
        using var client = _httpClientFactory.CreateClient("api");
        return await client.GetFromJsonAsync<Dictionary<long, CampaignCommentData>>($"api/campaigns/{campaignId}");
    }

    public async Task<CampaignCommentData> CreateCommentAsync(long campaignId, CampaignCommentData campaignCommentData)
    {
        using var client = _httpClientFactory.CreateClient("api");
        using var response = await client.PutAsJsonAsync($"api/campaigns/{campaignId}/createcomment", campaignCommentData);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CampaignCommentData>();
    }

    public async Task<CampaignCommentData> UpdateCommentAsync(CampaignCommentData campaignCommentData)
    {
        using var client = _httpClientFactory.CreateClient("api");
        using var response = await client.PatchAsJsonAsync("api/campaigns/updatecomment", campaignCommentData);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CampaignCommentData>();
    }

    public async Task AcceptCampaignAsync(long campaignId, bool force)
    {
        using var client = _httpClientFactory.CreateClient("api");
        using var response = await client.PostAsync($"api/campaigns/{campaignId}/accept/{force}", default);

        response.EnsureSuccessStatusCode();
    }

    public async Task RejectCampaignAsync(long campaignId)
    {
        using var client = _httpClientFactory.CreateClient("api");
        using var response = await client.PostAsync($"api/campaigns/{campaignId}/reject", default);

        response.EnsureSuccessStatusCode();
    }

    public async Task<NextRank> GetNextRankRoleForUserAsync(ulong subjectId)
    {
        return null;
        //var nextRank = await _promotionsService.GetNextRankRoleForUserAsync(subjectId);

        //if (nextRank is null)
        //    return new NextRank("None", "#607d8b");

        //var currentUser = await _userHelper.GetCurrentUserAsync();
        //var color = currentUser.Guild.Roles.First(r => r.Id == nextRank.Id).Color;
        //return new NextRank(nextRank.Name, color.ToString());
    }

    public async Task CreateAsync(PromotionCreationData creationData)
    {
        //await _promotionsService.CreateCampaignAsync(creationData.UserId, creationData.Comment);
    }
}
