using System;
using NUnit.Framework;
using Shouldly;

namespace Modix.Bot.Test;

public class TimeSpanTypeReaderTests
{
    internal static readonly string[] InvalidInputs
        =
        [
            "",
            "2",
            "z",
            "24z",
            "-1h",
            "2h-1m",
            "1hour",
            "h20",
            " 12h",
            "12h ",
            "test",
        ];

    [TestCaseSource(nameof(InvalidInputs))]
    public void TryParseTimeSpan_GivenInvalidInput_ReturnsFalse(string input)
    {
        var succeeded = TimeSpanTypeReader.TryParseTimeSpan(input, out _);

        succeeded.ShouldBeFalse();
    }

    internal static readonly object[] ValidInputs
        = new[]
        {
            new object[] { "0s", TimeSpan.Zero },
            ["1s", TimeSpan.FromSeconds(1)],
            ["60s", TimeSpan.FromSeconds(60)],
            ["61s", TimeSpan.FromSeconds(61)],

            ["0m", TimeSpan.Zero],
            ["1m", TimeSpan.FromMinutes(1)],
            ["60m", TimeSpan.FromMinutes(60)],
            ["61m", TimeSpan.FromMinutes(61)],

            ["0h", TimeSpan.Zero],
            ["1h", TimeSpan.FromHours(1)],
            ["24h", TimeSpan.FromHours(24)],
            ["25h", TimeSpan.FromHours(25)],

            ["0d", TimeSpan.Zero],
            ["1d", TimeSpan.FromDays(1)],
            ["31d", TimeSpan.FromDays(31)],
            ["32d", TimeSpan.FromDays(32)],

            ["0w", TimeSpan.Zero],
            ["1w", TimeSpan.FromDays(7)],
            ["4w", TimeSpan.FromDays(28)],
            ["100w", TimeSpan.FromDays(700)],

            ["0w0d0h0m0s", TimeSpan.Zero],
            ["1w1d1h1m1s", TimeSpan.FromDays(7) + TimeSpan.FromDays(1) + TimeSpan.FromHours(1) + TimeSpan.FromMinutes(1) + TimeSpan.FromSeconds(1)],
            ["4w31d24h60m60s", TimeSpan.FromDays(28) + TimeSpan.FromDays(31) + TimeSpan.FromHours(24) + TimeSpan.FromMinutes(60) + TimeSpan.FromSeconds(60)],
            ["100w32d25h61m61s", TimeSpan.FromDays(700) + TimeSpan.FromDays(32) + TimeSpan.FromHours(25) + TimeSpan.FromMinutes(61) + TimeSpan.FromSeconds(61)],
        };

    [TestCaseSource(nameof(ValidInputs))]
    public void TryParseTimeSpan_GivenValidInput_SuccessfullyParses(string input, TimeSpan expected)
    {
        var succeeded = TimeSpanTypeReader.TryParseTimeSpan(input, out var actual);

        succeeded.ShouldBeTrue();
        actual.ShouldBe(expected);
    }
}
