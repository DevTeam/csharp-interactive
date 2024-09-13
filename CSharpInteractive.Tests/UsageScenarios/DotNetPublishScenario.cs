// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
public class DotNetPublishScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Publish a project
        // {
        // Adds the namespace "HostApi" to use .NET build API
        // ## using HostApi;

        // Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
        var result = new DotNetNew("classlib", "-n", "MyLib", "--force", "-f", "net8.0")
            .Build()
            .EnsureSuccess();

        result.ExitCode.ShouldBe(0);

        // Publish the project, running a command like: "dotnet publish --framework net6.0" from the directory "MyLib"
        result = new DotNetPublish()
            .WithWorkingDirectory("MyLib")
            .WithFramework("net8.0")
            .Build()
            .EnsureSuccess();

        result.ExitCode.ShouldBe(0);
        // }
    }
}