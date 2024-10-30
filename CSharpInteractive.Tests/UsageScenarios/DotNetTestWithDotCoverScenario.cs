// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetTestWithDotCoverScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("mstest")
            .WithName("MyTests")
            .WithForce(true)
            .Run().EnsureSuccess();

        new DotNetNew()
            .WithTemplateName("tool-manifest")
            .Run().EnsureSuccess();
        
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Running tests under dotCover
        // {
        // ## using HostApi;

        new DotNetToolInstall()
            .WithLocal(true)
            .WithPackage("JetBrains.dotCover.GlobalTool")
            .Run().EnsureSuccess();

        // Creates a test command
        var test = new DotNetTest()
            .WithProject("MyTests");

        var dotCoverSnapshot = Path.Combine("MyTests", "dotCover.dcvr");
        var dotCoverReport = Path.Combine("MyTests", "dotCover.html");
        // Modifies the test command by putting "dotCover" in front of all arguments
        // to have something like "dotnet dotcover test ..."
        // and adding few specific arguments to the end
        var testUnderDotCover = test.Customize(cmd =>
            cmd.ClearArgs()
            + "dotcover"
            + cmd.Args
            + $"--dcOutput={dotCoverSnapshot}"
            + "--dcFilters=+:module=TeamCity.CSharpInteractive.HostApi;+:module=dotnet-csi"
            + "--dcAttributeFilters=System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage");

        // Runs tests under dotCover
        var result = testUnderDotCover
            .Build().EnsureSuccess();

        // The "result" variable provides details about a build
        result.ExitCode.ShouldBe(0, result.ToString());
        result.Tests.Count(i => i.State == TestState.Finished).ShouldBe(1, result.ToString());

        // Generates a HTML code coverage report.
        new DotNetCustom("dotCover", "report", $"--source={dotCoverSnapshot}", $"--output={dotCoverReport}", "--reportType=HTML")
            .Run().EnsureSuccess();
        // }

        // Check for a dotCover report
        File.Exists(dotCoverReport).ShouldBeTrue();
    }
}