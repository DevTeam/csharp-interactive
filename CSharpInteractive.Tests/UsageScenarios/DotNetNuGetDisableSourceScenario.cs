// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetNuGetDisableSourceScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact(Skip = "Fails on CI")]
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
        // $description=Disabling a NuGet source
        // {
        // ## using HostApi;

        new DotNetNuGetDisableSource()
            .WithName("TestSource")
            .Run().EnsureSuccess();
        // }
        
        new DotNetNuGetRemoveSource()
            .WithName("TestSource")
            .Run();
    }
}