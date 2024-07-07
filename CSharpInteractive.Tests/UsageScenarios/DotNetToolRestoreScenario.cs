// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo
namespace CSharpInteractive.Tests.UsageScenarios;

using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
public class DotNetToolRestoreScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=11 .NET build API
        // $priority=01
        // $description=Restore local tools
        // {
        // Adds the namespace "HostApi" to use .NET build API
        // ## using HostApi;

        var projectDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()[..4]);
        Directory.CreateDirectory(projectDir);
            
        // Creates a local tool manifest 
        new DotNetNew("tool-manifest").WithWorkingDirectory(projectDir).Run().EnsureSuccess();

        // Restore local tools
        new DotNetToolRestore().WithWorkingDirectory(projectDir).Run().EnsureSuccess();
        // }
        
        Directory.Delete(projectDir, true);
    }
}