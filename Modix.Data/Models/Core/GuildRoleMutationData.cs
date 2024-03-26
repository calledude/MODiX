﻿namespace Modix.Data.Models.Core;

/// <summary>
/// Describes an operation to create a <see cref="GuildRoleEntity"/>.
/// </summary>
public class GuildRoleMutationData
{
    /// <summary>
    /// See <see cref="GuildRoleEntity.Name"/>.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// See <see cref="GuildRoleEntity.Position"/>.
    /// </summary>
    public int Position { get; set; }

    internal static GuildRoleMutationData FromEntity(GuildRoleEntity entity)
        => new()
        {
            Name = entity.Name,
            Position = entity.Position
        };

    internal void ApplyTo(GuildRoleEntity entity)
    {
        entity.Name = Name;
        entity.Position = Position;
    }
}
