// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetNewScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Createing a new project, configuration file, or solution based on the specified template
        // {
        // ## using HostApi;

        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();
        // }
    }
}