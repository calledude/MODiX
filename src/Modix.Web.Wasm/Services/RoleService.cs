using System.Net.Http.Json;
using Modix.Web.Shared.Models.Common;
using Modix.Web.Shared.Services;

namespace Modix.Web.Wasm.Services;

public class RoleService : IRoleService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RoleService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Dictionary<ulong, RoleInformation>> GetRolesAsync()
    {
        using var client = _httpClientFactory.CreateClient("api");
        return await client.GetFromJsonAsync<Dictionary<ulong, RoleInformation>>("api/roles");
    }
}
