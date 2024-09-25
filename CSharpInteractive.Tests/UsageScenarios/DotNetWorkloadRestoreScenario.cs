// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetWorkloadRestoreScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact(Skip = "Inadequate permissions. Run the command with elevated privileges.")]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();
        
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Installing workloads needed for a project or a solution
        // {
        // ## using HostApi;

        new DotNetWorkloadRestore()
            .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
            .Run().EnsureSuccess();
        // }
    }
}