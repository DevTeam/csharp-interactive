// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
namespace CSharpInteractive.Tests.UsageScenarios;

using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
public class DotNetTestScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=11 .NET build API
        // $priority=01
        // $description=Test a project
        // {
        // Adds the namespace "HostApi" to use .NET build API
        // ## using HostApi;

        // Creates a new test project, running a command like: "dotnet new mstest -n MyTests --force"
        var result = new DotNetNew("mstest", "-n", "MyTests", "--force").Build();
        result.ExitCode.ShouldBe(0);

        // Runs tests via a command like: "dotnet test" from the directory "MyTests"
        result = new DotNetTest().WithWorkingDirectory("MyTests").Build();

        // The "result" variable provides details about a build
        result.ExitCode.ShouldBe(0);
        result.Summary.Tests.ShouldBe(1);
        result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1);
        // }
    }
}