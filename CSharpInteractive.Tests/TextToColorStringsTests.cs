// ReSharper disable StringLiteralTypo

namespace CSharpInteractive.Tests;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Usage", "xUnit1042:The member referenced by the MemberData attribute returns untyped data rows")]
public class TextToColorStringsTests
{
    [Theory]
    [MemberData(nameof(ConvertData))]
    public void ShouldConvert(string text, (ConsoleColor? color, string text)[] expected)
    {
        // Given
        var instance = CreateInstance();

        // When
        var actual = instance.Convert(text, ConsoleColor.Blue).ToArray();

        // Then
        actual.ShouldBe(expected);
    }

    public static IEnumerable<object[]> ConvertData => new List<object[]>
    {
        new object[] {"Usage: dotnet csi [options] [script-file.csx] [script-arguments]", new (ConsoleColor?, string)[] {(ConsoleColor.Blue, "Usage: dotnet csi [options] [script-file.csx] [script-arguments]")}},
        new object[] {"\e[36mAbc", new (ConsoleColor?, string)[] {(ConsoleColor.DarkCyan, "Abc")}},
        new object[] {"[36mAbc", new (ConsoleColor?, string)[] {(ConsoleColor.Blue, "[36mAbc")}},
        new object[] {"Abc", new (ConsoleColor?, string)[] {(ConsoleColor.Blue, "Abc")}},
        new object[] {"\e" + "AbcmXyz", new (ConsoleColor?, string)[] {(ConsoleColor.Blue, "\e" + "AbcmXyz")}},
        new object[] {"\e[AbcmXyz", new (ConsoleColor?, string)[] {(ConsoleColor.Blue, "\e[AbcmXyz")}},
        new object[] {"", new (ConsoleColor?, string)[] {(ConsoleColor.Blue, "")}},
        new object[] {"Xyz\e[36mAbc", new (ConsoleColor?, string)[] {(ConsoleColor.Blue, "Xyz"), (ConsoleColor.DarkCyan, "Abc")}},
        new object[] {"\e[93mXyz\e[36mAbc", new (ConsoleColor?, string)[] {(ConsoleColor.Yellow, "Xyz"), (ConsoleColor.DarkCyan, "Abc")}},
        new object[] {"\e[mXyz\e[36mAbc", new (ConsoleColor?, string)[] {(ConsoleColor.Blue, "\e[mXyz"), (ConsoleColor.DarkCyan, "Abc")}},
        new object[] {"\e[aamXyz\e[36mAbc", new (ConsoleColor?, string)[] {(ConsoleColor.Blue, "\e[aamXyz"), (ConsoleColor.DarkCyan, "Abc")}},
        new object[] {"\e[aa;36mXyz\e[36mAbc", new (ConsoleColor?, string)[] {(ConsoleColor.Blue, "\e[aa;36mXyz"), (ConsoleColor.DarkCyan, "Abc")}},
        new object[] {"\e[1;36mXyz\e[36mAbc", new (ConsoleColor?, string)[] {(ConsoleColor.DarkCyan, "Xyz"), (ConsoleColor.DarkCyan, "Abc")}},
        new object[] {"\e[36;1mXyz\e[36mAbc", new (ConsoleColor?, string)[] {(ConsoleColor.DarkCyan, "Xyz"), (ConsoleColor.DarkCyan, "Abc")}}
    };

    [Theory]
    [MemberData(nameof(SplitData))]
    public void ShouldSplit(string text, (string color, string text)[] expected)
    {
        // Given

        // When
        var actual = TextToColorStrings.Split(text).ToArray();

        // Then
        actual.ShouldBe(expected);
    }

    public static IEnumerable<object[]> SplitData => new List<object[]>
    {
        new object[] {"\e[93mXyz\e[36mAbc", new[] {("93", "Xyz"), ("36", "Abc")}},
        new object[] {"\e[93mXyz", new[] {("93", "Xyz")}},
        new object[] {"\e[93;22;44mXyz", new[] {("93;22;44", "Xyz")}},
        new object[] {"", new[] {("", "")}},
        new object[] {"   ", new[] {("", "   ")}},
        new object[] {"mmm", new[] {("", "mmm")}},
        new object[] {"##teamcity[[[", new[] {("", "##teamcity[[[")}},
        new object[] {"##teamcity[message text='\e|[34;1m Copying file from']", new[] {("", "##teamcity[message text='\e|[34;1m Copying file from']")}},
        new object[] {"JetBrains TeamCity C# script runner 1.0.0-dev net6.0", new[] {("", "JetBrains TeamCity C# script runner 1.0.0-dev net6.0")}},
        new object[] {"\e|[93mXyz", new[] {("", "\e|[93mXyz")}},
        new object[] {"\e[93amXyz", new[] {("", "\e[93amXyz")}},
        new object[] {"[93mXyz", new[] {("", "[93mXyz")}},
        new object[] {"\e[mXyz", new[] {("", "\e[mXyz")}},
        new object[] {"\e\e[93mXyz", new[] {("", "\e"), ("93", "Xyz")}},
        new object[] {"\e\e[mXyz", new[] {("", "\e"), ("", "\e\e[mXyz")}},
        new object[] {"\e[[93mXyz", new[] {("", "\e[[93mXyz")}},
        new object[] {"\e[[[93mXyz", new[] {("", "\e[[[93mXyz")}}
    };

    private static TextToColorStrings CreateInstance() =>
        new();
}