// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable InconsistentNaming

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetVSTestScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        const string path = "MyTests";
        
        new DotNetNew()
            .WithTemplateName("mstest")
            .WithName(path)
            .WithForce(true)
            .Run().EnsureSuccess();
        
        new DotNetBuild()
            .WithWorkingDirectory(path)
            .WithConfiguration("Release")
            .WithOutput("bin")
            .Build().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Running tests from the specified assemblies
        // {
        // ## using HostApi;

        // Runs tests
        var result = new VSTest()
            .AddTestFileNames(Path.Combine("bin", "MyTests.dll"))
            .WithWorkingDirectory(path)
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

        // Builds the test project, running a command like: "dotnet build -c Release" from the directory "MyTests"
        new DotNetBuild()
            .WithWorkingDirectory("MyTests")
            .WithConfiguration("Release")
            .WithOutput("MyOutput")
            .Build().EnsureSuccess();

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