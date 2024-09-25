// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetToolRestoreScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Installing the .NET local tools that are in scope for the current directory
        // {
        // ## using HostApi;

        // Creates a local tool manifest 
        new DotNetNew()
            .WithTemplateName("tool-manifest")
            .Run().EnsureSuccess();

        new DotNetToolRestore()
            .Run().EnsureSuccess();
        // }
    }
}