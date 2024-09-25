// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetNuGetDeleteScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();
        
        new DotNetPack()
            .WithWorkingDirectory("MyLib")
            .WithOutput("packages")
            .Build().EnsureSuccess();

        var repoUrl = Path.GetFullPath(".");
        
        new DotNetNuGetPush()
            .WithWorkingDirectory("MyLib")
            .WithPackage(Path.Combine("packages", "MyLib.1.0.0.nupkg"))
            .WithSource(repoUrl)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Deleting a NuGet package to the server
        // {
        // ## using HostApi;

        new DotNetNuGetDelete()
            .WithPackage("MyLib")
            .WithPackageVersion("1.0.0")
            .WithSource(repoUrl)
            .Run().EnsureSuccess();
        // }
    }
}