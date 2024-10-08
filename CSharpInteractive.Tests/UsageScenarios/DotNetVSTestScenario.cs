// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable InconsistentNaming

namespace CSharpInteractive.Tests.UsageScenarios;

using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
public class DotNetVSTestScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Test an assembly
        // {
        // Adds the namespace "HostApi" to use .NET build API
        // ## using HostApi;

        // Creates a new test project, running a command like: "dotnet new mstest -n MyTests --force"
        var result = new DotNetNew("mstest", "-n", "MyTests", "--force")
            .Build()
            .EnsureSuccess();

        result.ExitCode.ShouldBe(0);

        // Builds the test project, running a command like: "dotnet build -c Release" from the directory "MyTests"
        result = new DotNetBuild()
            .WithWorkingDirectory("MyTests")
            .WithConfiguration("Release")
            .WithOutput("MyOutput")
            .Build()
            .EnsureSuccess();

        result.ExitCode.ShouldBe(0);

        // Runs tests via a command like: "dotnet vstest" from the directory "MyTests"
        result = new VSTest()
            .AddTestFileNames(Path.Combine("MyOutput", "MyTests.dll"))
            .WithWorkingDirectory("MyTests")
            .Build()
            .EnsureSuccess();

        // The "result" variable provides details about a build
        result.ExitCode.ShouldBe(0);
        result.Summary.Tests.ShouldBe(1);
        result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1);
        // }
    }

    [Fact]
    public void RunAsCommandLine()
    {
        // Creates a new test project, running a command like: "dotnet new mstest -n MyTests --force"
        var result = new DotNetNew("mstest", "-n", "MyTests", "--force")
            .Build()
            .EnsureSuccess();

        result.ExitCode.ShouldBe(0);

        // Builds the test project, running a command like: "dotnet build -c Release" from the directory "MyTests"
        result = new DotNetBuild()
            .WithWorkingDirectory("MyTests")
            .WithConfiguration("Release")
            .WithOutput("MyOutput")
            .Build()
            .EnsureSuccess();

        result.ExitCode.ShouldBe(0);

        // Runs tests via a command like: "dotnet vstest" from the directory "MyTests"
        var lines = new List<string>();
        var exitCode = new VSTest()
            .AddTestFileNames(Path.Combine("MyOutput", "MyTests.dll"))
            .WithWorkingDirectory("MyTests")
            .Run(i => lines.Add(i.Line))
            .EnsureSuccess()
            .ExitCode;

        lines.Count(i => i.Contains("##teamcity[")).ShouldBe(0);
        exitCode.ShouldBe(0);
    }
}