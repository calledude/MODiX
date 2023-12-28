namespace Modix.Data.Models.Core
{
    public class MessageCountPerChannel
    {
        public ulong ChannelId { get; set; }
        public required string ChannelName { get; set; }
        public int MessageCount { get; set; }
    }
}
