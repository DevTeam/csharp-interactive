// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetNuGetRemoveSourceScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNuGetRemoveSource()
            .WithName("TestSource")
            .Run();
        
        var source = Path.GetFullPath(".");
        
        new DotNetNuGetAddSource()
            .WithName("TestSource")
            .WithSource(source)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Removing an existing source from your NuGet configuration files
        // {
        // ## using HostApi;

        new DotNetNuGetRemoveSource()
            .WithName("TestSource")
            .Run().EnsureSuccess(); 
        // }
    }
}