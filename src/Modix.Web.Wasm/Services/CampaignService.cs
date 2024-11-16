using System.Net.Http.Json;
using Modix.Web.Shared.Models.Promotions;
using Modix.Web.Shared.Services;

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
        using var client = _httpClientFactory.CreateClient("api");
        return await client.GetFromJsonAsync<NextRank>($"api/campaigns/{subjectId}/nextrank");
    }

    public async Task CreateAsync(PromotionCreationData creationData)
    {
        using var client = _httpClientFactory.CreateClient("api");
        using var response = await client.PutAsJsonAsync("api/campaigns/create", creationData);

        response.EnsureSuccessStatusCode();
    }
}
