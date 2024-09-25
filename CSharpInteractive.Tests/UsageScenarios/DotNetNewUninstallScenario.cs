// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetNewUninstallScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        var exitCode = new DotNetNewInstall()
            .WithPackage("Pure.DI.Templates")
            .Run().ExitCode;

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Uninstalling a template package
        // {
        // ## using HostApi;

        new DotNetNewUninstall()
            .WithPackage("Pure.DI.Templates")
            .Run();
        // }

        if (exitCode == 0)
        {
            new DotNetNewInstall()
                .WithPackage("Pure.DI.Templates")
                .Run();
        }
    }
}