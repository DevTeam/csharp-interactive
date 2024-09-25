// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetNuGetUpdateSourceScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNuGetRemoveSource()
            .WithName("TestSource")
            .Run();
        
        var source = Path.GetFullPath(".");
        var newSource = Path.GetFullPath(".");
        
        new DotNetNuGetAddSource()
            .WithName("TestSource")
            .WithSource(source)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Updating a NuGet source
        // {
        // ## using HostApi;

        new DotNetNuGetUpdateSource()
            .WithName("TestSource")
            .WithSource(newSource)
            .Run().EnsureSuccess();
        // }
        
        new DotNetNuGetRemoveSource()
            .WithName("TestSource")
            .Run();
    }
}