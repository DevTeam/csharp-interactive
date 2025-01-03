// ReSharper disable StringLiteralTypo
// ReSharper disable RedundantUsingDirective

namespace CSharpInteractive.Tests.Integration;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class MsDITests
{
    [Fact]
    public void ShouldSupportDIFromTheBox()
    {
        // Given

        // When
        var result = TestTool.Run(
            "using HostApi;",
            "using Microsoft.Extensions.DependencyInjection;",
            "GetService<IServiceCollection>().BuildServiceProvider().GetRequiredService<IBuildRunner>()");

        // Then
        result.StdErr.ShouldBeEmpty(result.ToString());
        result.ExitCode.ShouldBe(0, result.ToString());
    }
}