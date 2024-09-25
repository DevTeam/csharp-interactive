// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetWorkloadRepairScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact(Skip = "Works too slow")]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Repairing workloads installations
        // {
        // ## using HostApi;

        new DotNetWorkloadRepair()
            .Run().EnsureSuccess();
        // }
    }
}