// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetToolListScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Printing all .NET tools of the specified type currently installed
        // {
        // ## using HostApi;

        new DotNetToolList()
            .WithLocal(true)
            .Run().EnsureSuccess();
        
        new DotNetToolList()
            .WithGlobal(true)
            .Run().EnsureSuccess();
        // }
    }
}