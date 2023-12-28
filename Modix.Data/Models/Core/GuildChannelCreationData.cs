﻿namespace Modix.Data.Models.Core
{
    /// <summary>
    /// Describes an operation to create a <see cref="GuildChannelEntity"/>.
    /// </summary>
    public class GuildChannelCreationData
    {
        /// <summary>
        /// See <see cref="GuildChannelEntity.ChannelId"/>.
        /// </summary>
        public ulong ChannelId { get; set; }

        /// <summary>
        /// See <see cref="GuildChannelEntity.GuildId"/>.
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        /// See <see cref="GuildChannelEntity.Name"/>.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// See <see cref="GuildChannelEntity.ParentChannelId"/>.
        /// </summary>
        public ulong? ParentChannelId { get; set; }

        internal GuildChannelEntity ToEntity()
            => new()
            {
                ChannelId = ChannelId,
                GuildId = GuildId,
                Name = Name,
                ParentChannelId = ParentChannelId,
            };
    }
}
