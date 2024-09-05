// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;
using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetRunScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Run a project
        // {
        // Adds the namespace "HostApi" to use .NET build API
        // ## using HostApi;

        // Creates a new console project, running a command like: "dotnet new console -n MyApp --force"
        var result = new DotNetNew("console", "-n", "MyApp", "--force")
            .Build()
            .EnsureSuccess();
        
        result.ExitCode.ShouldBe(0);

        // Runs the console project using a command like: "dotnet run" from the directory "MyApp"
        var stdOut = new List<string>();
        result = new DotNetRun().WithWorkingDirectory("MyApp")
            .Build(message => stdOut.Add(message.Text))
            .EnsureSuccess();
        
        result.ExitCode.ShouldBe(0);

        // Checks StdOut
        stdOut.ShouldBe(new[] {"Hello, World!"});
        // }
    }
}