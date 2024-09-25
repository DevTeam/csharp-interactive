// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetNuGetConfigPathsScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(HasSdk("8.0.10"));
        var configFile = Path.GetFullPath("nuget.config");
        File.WriteAllText(configFile, "<configuration></configuration>");

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Printing nuget configuration files currently being applied to a directory
        // {
        // ## using HostApi;

        var configPaths = new List<string>();
        new DotNetNuGetConfigPaths()
            .Run(output => configPaths.Add(output.Line)).EnsureSuccess();
        // }

        configPaths.Count.ShouldBeGreaterThan(0);
        configPaths.Contains(Path.GetFullPath(configFile));
    }
}