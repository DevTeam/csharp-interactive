// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;
using NuGet.Versioning;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetNuGetConfigSetScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(HasSdk("8.0.10"));
        var configFile = Path.GetFullPath("nuget.config");
        File.WriteAllText(configFile, "<configuration></configuration>");
        
        new DotNetNuGetConfigSet()
            .WithConfigFile(configFile)
            .WithConfigKey("repositoryPath")
            .WithConfigValue("MyValue")
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Getting a value of a specified NuGet configuration setting
        // {
        // ## using HostApi;

        string? repositoryPath = default;
        new DotNetNuGetConfigGet()
            .WithConfigKey("repositoryPath")
            .Run(output => repositoryPath = output.Line).EnsureSuccess();
        // }

        repositoryPath.ShouldBe("MyValue");
    }
}