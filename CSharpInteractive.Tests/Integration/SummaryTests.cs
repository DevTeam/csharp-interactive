// ReSharper disable StringLiteralTypo
// ReSharper disable RedundantUsingDirective

namespace CSharpInteractive.Tests.Integration;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class SummaryTests
{
    [Fact]
    public void ShouldInterpretAsSucceededWhenHasStdErr()
    {
        // Given

        // When
        var result = TestTool.Run("Console.Error.WriteLine(\"StdErr\");");

        // Then
        result.ExitCode.ShouldBe(0, result.ToString());
        result.StdOut.Contains("Running succeeded.").ShouldBeTrue(result.ToString());
        result.StdOut.Contains("StdErr").ShouldBeFalse(result.ToString());
        result.StdErr.Contains("StdErr").ShouldBeTrue(result.ToString());
    }

    [Fact]
    public void ShouldInterpretAsFailedWhenError()
    {
        // Given

        // When
        var result = TestTool.Run("Error(\"StdErr\");");

        // Then
        result.ExitCode.ShouldBe(1, result.ToString());
        result.StdOut.Contains("Running FAILED.").ShouldBeTrue(result.ToString());
        result.StdErr.Any(i => i.Contains("StdErr")).ShouldBeTrue(result.ToString());
        result.StdOut.Contains("StdErr").ShouldBeFalse(result.ToString());
    }
}