namespace Modix.Data.Models.Core;

public class PerUserMessageCount
{
    public required string Username { get; set; }

    public required string Discriminator { get; set; }

    public int Rank { get; set; }

    public int MessageCount { get; set; }

    public bool IsCurrentUser { get; set; }
}
