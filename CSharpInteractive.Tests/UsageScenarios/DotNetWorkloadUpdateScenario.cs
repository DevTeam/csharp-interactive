// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped")]
public class DotNetWorkloadUpdateScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact(Skip = "Inadequate permissions. Run the command with elevated privileges.")]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Updating installed workloads
        // {
        // ## using HostApi;

        new DotNetWorkloadUpdate()
            .Run().EnsureSuccess();
        // }
    }
}