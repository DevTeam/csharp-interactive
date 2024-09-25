// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable InconsistentNaming

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetMSBuildVSTestScenario(ITestOutputHelper output) : BaseScenario(output)
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
        // $description=Test a project using the MSBuild VSTest target
        // {
        // ## using HostApi;

        // Runs tests via a command
        var result = new MSBuild()
            .WithTarget("VSTest")
            .WithWorkingDirectory("MyTests")
            .Build().EnsureSuccess();

        // The "result" variable provides details about a build
        result.ExitCode.ShouldBe(0, result.ToString());
        result.Summary.Tests.ShouldBe(1, result.ToString());
        result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1, result.ToString());
        // }
    }
}