using System.Security.Claims;
using Discord.WebSocket;
using Microsoft.AspNetCore.Authentication;
using Modix.Services.Core;
using Modix.Web.Models;

namespace Modix.Web.Services;

public class UserHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DiscordSocketClient _discordSocketClient;
    private readonly IAuthorizationService _authorizationService;

    public UserHelper(IHttpContextAccessor httpContextAccessor, DiscordSocketClient discordSocketClient, IAuthorizationService authorizationService)
    {
        _httpContextAccessor = httpContextAccessor;
        _discordSocketClient = discordSocketClient;
        _authorizationService = authorizationService;
    }

    public async Task<SocketGuildUser?> GetAuthenticatedUserAsync()
    {
        if (!_discordSocketClient.Guilds.Any())
            return null;

        if (_httpContextAccessor.HttpContext is null)
            throw new InvalidOperationException(nameof(_httpContextAccessor.HttpContext) + " is unavailable");

        if (_httpContextAccessor.HttpContext.User is null)
        {
            await _httpContextAccessor.HttpContext.ChallengeAsync();
            return null;
        }

        var user = _httpContextAccessor.HttpContext.User;
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!ulong.TryParse(userId, out var userSnowflake))
        {
            await _httpContextAccessor.HttpContext.ChallengeAsync();
            return null;
        }

        var request = _httpContextAccessor.HttpContext.Request;
        var guildCookie = request.Cookies[CookieConstants.SelectedGuild];
        SocketGuild guildToSearch;

        if (!string.IsNullOrWhiteSpace(guildCookie))
        {
            var guildId = ulong.Parse(guildCookie);
            guildToSearch = _discordSocketClient.GetGuild(guildId);
        }
        else
        {
            guildToSearch = _discordSocketClient.Guilds.First();
        }

        var socketUser = guildToSearch.GetUser(userSnowflake);

        if (socketUser is null)
        {
            await _httpContextAccessor.HttpContext.ChallengeAsync();
            return null;
        }

        await _authorizationService.OnAuthenticatedAsync(socketUser.Id, socketUser.Guild.Id, [.. socketUser.Roles.Select(x => x.Id)]);

        return socketUser;
    }
}
