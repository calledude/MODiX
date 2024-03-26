﻿using System;
using System.Linq.Expressions;

using Modix.Data.ExpandableQueries;
using Modix.Data.Models.Core;

namespace Modix.Data.Models.Moderation
{
    /// <summary>
    /// Describes a partial view of an <see cref="DeletedMessageEntity"/>, for use within the context of another projected model.
    /// </summary>
    public class DeletedMessageBrief
    {
        /// <summary>
        /// See <see cref="DeletedMessageEntity.MessageId"/>.
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// See <see cref="DeletedMessageEntity.Channel"/>.
        /// </summary>
        public required GuildChannelBrief Channel { get; set; }

        /// <summary>
        /// See <see cref="DeletedMessageEntity.Author"/>.
        /// </summary>
        public required GuildUserBrief Author { get; set; }

        /// <summary>
        /// See <see cref="DeletedMessageEntity.Content"/>.
        /// </summary>
        public required string Content { get; set; }

        /// <summary>
        /// See <see cref="DeletedMessageEntity.Reason"/>.
        /// </summary>
        public required string Reason { get; set; }

        /// <summary>
        /// See <see cref="DeletedMessageEntity.BatchId"/>.
        /// </summary>
        public long? BatchId { get; set; }

        [ExpansionExpression]
        internal static Expression<Func<DeletedMessageEntity, DeletedMessageBrief>> FromEntityProjection
            = entity => new DeletedMessageBrief()
            {
                Id = entity.MessageId,
                Channel = entity.Channel.Project(GuildChannelBrief.FromEntityProjection),
                Author = entity.Author.Project(GuildUserBrief.FromEntityProjection),
                Content = entity.Content,
                Reason = entity.Reason,
                BatchId = entity.BatchId,
            };
    }
}
