// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetWorkloadConfigScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(HasSdk("8.0.10"));

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Enabling or disabling workload-set update mode
        // {
        // ## using HostApi;

        new DotNetWorkloadConfig()
            .WithUpdateMode(DotNetWorkloadUpdateMode.WorkloadSet)
            .Run().EnsureSuccess();
        // }
    }
}