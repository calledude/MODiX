using Microsoft.Extensions.Caching.Memory;
using Modix.Web.Shared.Models.Common;
using Modix.Web.Shared.Services;

namespace Modix.Web.Services;

public class RoleService : IRoleService
{
    private readonly IMemoryCache _memoryCache;
    private readonly UserHelper _userHelper;

    public RoleService(IMemoryCache memoryCache, UserHelper userHelper)
    {
        _memoryCache = memoryCache;
        _userHelper = userHelper;
    }

    public async Task<Dictionary<ulong, RoleInformation>> GetRolesAsync()
    {
        var roles = await _memoryCache.GetOrCreateAsync("roles", async cacheEntry =>
        {
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);

            var currentUser = await _userHelper.GetAuthenticatedUserAsync();
            return currentUser.Guild.Roles
                .Select(x => new RoleInformation(x.Id, x.Name, x.Color.ToString()))
                .ToDictionary(x => x.Id);
        });

        return roles!;
    }
}
