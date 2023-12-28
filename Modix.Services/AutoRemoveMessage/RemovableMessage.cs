using Discord;

namespace Modix.Services.AutoRemoveMessage
{
    public class RemovableMessage
    {
        public required IMessage Message { get; init; }

        public required IUser[] Users { get; init; }
    }
}
