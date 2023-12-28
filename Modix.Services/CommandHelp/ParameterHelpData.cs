using System;
using System.Collections.Generic;

namespace Modix.Services.CommandHelp
{
    public class ParameterHelpData
    {
        public required string Name { get; init; }

        public required string Summary { get; init; }

        public required string Type { get; init; }

        public bool IsOptional { get; set; }

        public required IReadOnlyCollection<string> Options { get; init; }

        public static ParameterHelpData FromParameterInfo(Discord.Commands.ParameterInfo parameter)
            => BuildHelpData(parameter.Name, parameter.Summary, parameter.Type, !parameter.IsOptional);

        public static ParameterHelpData FromParameterInfo(Discord.Interactions.SlashCommandParameterInfo parameter)
            => BuildHelpData(parameter.Name, parameter.Description, parameter.ParameterType, parameter.IsRequired);

        private static ParameterHelpData BuildHelpData(string name, string description, Type parameterType, bool required)
        {
            var isNullable = parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(Nullable<>);
            parameterType = isNullable ? parameterType.GetGenericArguments()[0] : parameterType;
            var typeName = parameterType.Name;

            if (parameterType.IsInterface && parameterType.Name.StartsWith('I'))
            {
                typeName = typeName[1..];
            }

            return new()
            {
                Name = name,
                Summary = description,
                Type = typeName,
                IsOptional = isNullable || !required,
                Options = parameterType.IsEnum
                            ? parameterType.GetEnumNames()
                            : Array.Empty<string>(),
            };
        }
    }
}
