// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetToolUninstallScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("tool-manifest")
            .Run().EnsureSuccess();
        
        new DotNetToolInstall()
            .WithLocal(true)
            .WithPackage("dotnet-csi")
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Uninstalling the specified .NET tool
        // {
        // ## using HostApi;

        new DotNetToolUninstall()
            .WithPackage("dotnet-csi")
            .Run().EnsureSuccess();
        // }
    }
}