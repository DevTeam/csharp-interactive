// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable UnusedVariable
namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[Trait("Docker", "True")]
public class DockerRunScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=06 Docker CLI
        // $priority=01
        // $description=Running in docker
        // {
        // ## using HostApi;

        // Creates some command line to run in a docker container
        var cmd = new CommandLine("whoami");

        // Runs the command line in a docker container
        var result = new DockerRun(cmd, "mcr.microsoft.com/dotnet/sdk")
            .WithAutoRemove(true)
            .Run().EnsureSuccess();
        // }
    }
}