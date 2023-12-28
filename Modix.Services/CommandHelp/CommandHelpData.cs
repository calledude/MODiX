using System.Collections.Generic;
using System.Linq;

namespace Modix.Services.CommandHelp
{
    public class CommandHelpData
    {
        public required string Name { get; init; }

        public required string Summary { get; init; }

        public required IReadOnlyCollection<string> Aliases { get; init; }

        public required IReadOnlyCollection<ParameterHelpData> Parameters { get; init; }

        public bool IsSlashCommand { get; init; }

        public static CommandHelpData FromCommandInfo(Discord.Commands.CommandInfo command)
            => new()
            {
                Name = command.Name,
                Summary = command.Summary,
                Aliases = command.Aliases,
                Parameters = command.Parameters
                        .Select(x => ParameterHelpData.FromParameterInfo(x))
                        .ToArray(),
            };

        public static CommandHelpData FromCommandInfo(Discord.Interactions.SlashCommandInfo command)
            => new()
            {
                Name = command.ToString(),
                Summary = command.Description,
                Aliases = new[] { command.ToString() },
                Parameters = command.Parameters
                        .Select(x => ParameterHelpData.FromParameterInfo(x))
                        .ToArray(),
                IsSlashCommand = true,
            };
    }
}
