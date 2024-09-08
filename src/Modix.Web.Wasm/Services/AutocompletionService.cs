using System.Net.Http.Json;
using Modix.Web.Shared.Models.Common;
using Modix.Web.Shared.Services;

namespace Modix.Web.Wasm.Services;

public class AutocompletionService : IAutocompletionService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AutocompletionService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<ModixUser>?> AutocompleteUsersAsync(string query)
    {
        using var client = _httpClientFactory.CreateClient("api");
        return await client.GetFromJsonAsync<ModixUser[]>($"api/autocomplete/users/{query}");
    }
}
