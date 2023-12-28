using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Discord;
using Modix.Services.Utilities;

namespace Modix.Models
{
    public class ModixUser
    {
        public required string Name { get; init; }
        public ulong UserId { get; set; }
        public string? AvatarHash { get; set; }
        public List<string>? Claims { get; set; }
        public ulong SelectedGuild { get; set; }

        public static ModixUser FromClaimsPrincipal(string name, IEnumerable<Claim> claims)
        {
            var ret = new ModixUser
            {
                Name = name,
                UserId = ulong.Parse(claims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value ?? "0"),
                Claims = claims.Where(d => d.Type == ClaimTypes.Role).Select(d => d.Value).ToList()
            };

            return ret;
        }

        public static ModixUser FromIGuildUser(IGuildUser user)
        {
            var ret = new ModixUser
            {
                Name = user.GetDisplayName(),
                UserId = user.Id,
                AvatarHash = user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl()
            };

            return ret;
        }
    }
}
