// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetBuildScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("xunit")
            .WithName("MyTests")
            .WithForce(true)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Building a project
        // {
        // ## using HostApi;

        var messages = new List<BuildMessage>();
        var result = new DotNetBuild()
            .WithWorkingDirectory("MyTests")
            .Build(message => messages.Add(message)).EnsureSuccess();
        
        result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse(result.ToString());
        result.ExitCode.ShouldBe(0, result.ToString());
        // }
        
        messages.Count.ShouldBeGreaterThan(0, result.ToString());
        result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse(result.ToString());

        result = new DotNetTest()
            .WithWorkingDirectory("MyTests")
            .Build().EnsureSuccess();
        
        result.ExitCode.ShouldBe(0, result.ToString());
        result.Summary.Tests.ShouldBe(1, result.ToString());
        result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1, result.ToString());
    }
}