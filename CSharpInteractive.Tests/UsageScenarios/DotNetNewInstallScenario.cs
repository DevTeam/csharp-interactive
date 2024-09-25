// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetNewInstallScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        var exitCode = new DotNetNewUninstall()
            .WithPackage("Pure.DI.Templates")
            .Run().ExitCode;

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Installing a template package
        // {
        // ## using HostApi;

        new DotNetNewInstall()
            .WithPackage("Pure.DI.Templates")
            .Run().EnsureSuccess();
        // }

        if (exitCode == 0)
        {
            new DotNetNewUninstall()
                .WithPackage("Pure.DI.Templates")
                .Run();
        }
    }
}