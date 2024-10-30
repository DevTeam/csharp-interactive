// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetCleanScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();
        
        new DotNetBuild()
            .WithWorkingDirectory("MyLib")
            .Build().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Cleaning a project
        // {
        // ## using HostApi;
        
        // Clean the project, running a command like: "dotnet clean" from the directory "MyLib"
        new DotNetClean()
            .WithWorkingDirectory("MyLib")
            .Build().EnsureSuccess();
        // }
    }
}