// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped")]
public class DotNetNuGetEnableSourceScenario(ITestOutputHelper output) : BaseScenario(output)
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
        
        new DotNetNuGetDisableSource()
            .WithName("TestSource")
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Enabling a NuGet source
        // {
        // ## using HostApi;

        new DotNetNuGetEnableSource()
            .WithName("TestSource")
            .Run().EnsureSuccess();
        // }
        
        new DotNetNuGetRemoveSource()
            .WithName("TestSource")
            .Run();
    }
}