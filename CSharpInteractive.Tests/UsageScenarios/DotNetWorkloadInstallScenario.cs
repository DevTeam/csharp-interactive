// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetWorkloadInstallScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact(Skip = "Inadequate permissions. Run the command with elevated privileges.")]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Installing optional workloads
        // {
        // ## using HostApi;

        new DotNetWorkloadInstall()
            .AddWorkloads("aspire")
            .Run().EnsureSuccess();
        // }
    }
}