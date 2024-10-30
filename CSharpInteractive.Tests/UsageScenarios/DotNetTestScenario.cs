// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetTestScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("mstest")
            .WithName("MyTests")
            .WithForce(true)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Testing from the specified project
        // {
        // ## using HostApi;

        // Runs tests
        var result = new DotNetTest()
            .WithWorkingDirectory("MyTests")
            .Build().EnsureSuccess();

        // The "result" variable provides details about build and tests
        result.ExitCode.ShouldBe(0, result.ToString());
        result.Summary.Tests.ShouldBe(1, result.ToString());
        result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1, result.ToString());
        // }
    }

    [Fact]
    public void RunAsCommandLine()
    {
        // Creates a new test project, running a command like: "dotnet new mstest -n MyTests --force"
        new DotNetNew()
            .WithTemplateName("mstest")
            .WithName("MyTests")
            .WithForce(true)
            .Build().EnsureSuccess();
        
        // Runs tests via a command like: "dotnet test" from the directory "MyTests"
        var lines = new List<string>();
        new DotNetTest()
            .WithWorkingDirectory("MyTests")
            .Run(i => lines.Add(i.Line))
            .EnsureSuccess();

        lines.Count(i => i.Contains("##teamcity[")).ShouldBe(0);
    }
}