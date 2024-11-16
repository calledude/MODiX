using Discord;
using Modix.Data.Utilities;
using Modix.Services.Core;
using Modix.Services.Utilities;
using Modix.Web.Shared.Models.Common;
using Modix.Web.Shared.Services;

namespace Modix.Web.Services;

public class AutocompletionService : IAutocompletionService
{
    private readonly UserHelper _userHelper;
    private readonly IUserService _userService;

    public AutocompletionService(UserHelper userHelper, IUserService userService)
    {
        _userHelper = userHelper;
        _userService = userService;
    }

    public async Task<IEnumerable<ModixUser>?> AutocompleteUsersAsync(string query)
    {
        var currentUser = await _userHelper.GetAuthenticatedUserAsync();
        var result = currentUser.Guild.Users
            .Where(d => d.Username.OrdinalContains(query) || d.Id.ToString() == query)
            .Take(10)
            .Select(FromIGuildUser);

        if (result.Any() || !ulong.TryParse(query, out var userId))
            return result;

        var user = await _userService.GetUserInformationAsync(currentUser.Guild.Id, userId);

        if (user is not null)
            return [FromNonGuildUser(user)];

        return [];
    }

    private static ModixUser FromIGuildUser(IGuildUser user) => new()
    {
        Name = user.GetDisplayName(),
        UserId = user.Id,
        AvatarUrl = user.GetDisplayAvatarUrl() ?? user.GetDefaultAvatarUrl()
    };

    private static ModixUser FromNonGuildUser(IUser user) => new()
    {
        Name = user.GetDisplayName(),
        UserId = user.Id,
        AvatarUrl = user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl()
    };
}
