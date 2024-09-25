// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetNuGetConfigGetScenario(ITestOutputHelper output) : BaseScenario(output)
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
        // $description=Sets the value of a specified NuGet configuration setting
        // {
        // ## using HostApi;

        new DotNetNuGetConfigSet()
            .WithConfigFile(configFile)
            .WithConfigKey("repositoryPath")
            .WithConfigValue("MyValue")
            .Run().EnsureSuccess();
        // }

        string? repositoryPath = default;
        new DotNetNuGetConfigGet()
            .WithConfigKey("repositoryPath")
            .Run(output => repositoryPath = output.Line).EnsureSuccess();

        repositoryPath.ShouldBe("MyValue");
    }
}