using System.Collections.Generic;
using System.Linq;

using Modix.Data.Models.Core;

namespace Modix.Data.Test.TestData
{
    public static class GuildChannels
    {
        public static readonly IEnumerable<GuildChannelEntity> Entities
            = new GuildChannelEntity[]
            {
                new()
                {
                    ChannelId = 1,
                    GuildId = 1,
                    Name = "GuildChannel1"
                },
                new()
                {
                    ChannelId = 2,
                    GuildId = 1,
                    Name = "GuildChannel2"
                },
                new()
                {
                    ChannelId = 3,
                    GuildId = 2,
                    Name = "GuildChannel3"
                }
            };

        public static GuildChannelEntity Clone(this GuildChannelEntity entity)
            => new()
            {
                ChannelId = entity.ChannelId,
                GuildId = entity.GuildId,
                Name = entity.Name
            };

        public static IEnumerable<GuildChannelEntity> Clone(this IEnumerable<GuildChannelEntity> entities)
            => entities.Select(Clone);

        public static readonly IEnumerable<GuildChannelCreationData> NewCreations
            = new GuildChannelCreationData[]
            {
                new()
                {
                    ChannelId = 4,
                    GuildId = 3,
                    Name = "NewGuildChannel4"
                }
            };

        public static readonly IEnumerable<GuildChannelCreationData> ExistingCreations
            = new GuildChannelCreationData[]
            {
                new()
                {
                    ChannelId = 1,
                    GuildId = 1,
                    Name = "ExistingGuildChannel1"
                },
                new()
                {
                    ChannelId = 2,
                    GuildId = 1,
                    Name = "ExistingGuildChannel2"
                },
                new()
                {
                    ChannelId = 3,
                    GuildId = 2,
                    Name = "ExistingGuildChannel3"
                },
                new()
                {
                    ChannelId = 3,
                    GuildId = 3,
                    Name = "ExistingGuildChannel4"
                }
            };
    }
}
