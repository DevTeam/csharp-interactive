// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetStoreScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Storing the specified assemblies in the runtime package store.
        // {
        // ## using HostApi;

        new DotNetStore()
            .AddManifests(Path.Combine("MyLib", "MyLib.csproj"))
            .WithFramework("net8.0")
            .WithRuntime("win-x64")
            .Build();

        // }

        new DotNetSdkCheck().Run();
    }
}