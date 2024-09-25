// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetBuildServerShutdownScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=02
        // $description=Shuts down build servers
        // {
        // ## using HostApi;

        // Shuts down all build servers that are started from dotnet.
        new DotNetBuildServerShutdown()
            .Run().EnsureSuccess();
        // }
    }
}