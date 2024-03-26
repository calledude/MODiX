﻿namespace Modix.Data.Models.Emoji;

public class EmojiUsageStatistics
{
    public required EphemeralEmoji Emoji { get; set; }

    public int Rank { get; set; }

    public int Uses { get; set; }

    internal static EmojiUsageStatistics FromDto(EmojiStatsDto emojiStatsDto)
        => new()
        {
            Emoji = EphemeralEmoji.FromRawData(emojiStatsDto.EmojiName, emojiStatsDto.EmojiId, emojiStatsDto.IsAnimated),
            Rank = emojiStatsDto.Rank,
            Uses = emojiStatsDto.Uses,
        };
}
