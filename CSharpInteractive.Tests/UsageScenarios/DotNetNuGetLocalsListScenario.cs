// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetNuGetLocalsListScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Printing the location of the specified NuGet cache type
        // {
        // ## using HostApi;

        new DotNetNuGetLocalsList()
            .WithCacheLocation(NuGetCacheLocation.GlobalPackages)
            .Run().EnsureSuccess();
        // }
    }
}