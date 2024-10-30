// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetNuGetWhyScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(HasSdk("8.0.10"));
        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();
        
        new DotNetPack()
            .WithWorkingDirectory("MyLib")
            .AddProps(("version", "1.2.3"))
            .WithOutput(Path.GetFullPath("."))
            .Build().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Printing a dependency graph for NuGet package
        // {
        // ## using HostApi;

        new DotNetNuGetWhy()
            .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
            .WithPackage("MyLib.1.2.3.nupkg")
            .Run().EnsureSuccess();
        // }
    }
}