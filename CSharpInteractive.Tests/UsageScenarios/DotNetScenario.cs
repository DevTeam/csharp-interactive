// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;
using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Run a dotnet application
        // {
        // Adds the namespace "HostApi" to use .NET build API
        // ## using HostApi;

        // Creates a new console project, running a command like: "dotnet new console -n MyApp --force"
        new DotNetNew()
            .WithTemplateName("console")
            .WithName("MyApp")
            .WithForce(true)
            .Build().EnsureSuccess();

        var tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        new DotNetPublish()
            .WithWorkingDirectory("MyApp")
            .WithOutput(tempDirectory)
            .Build().EnsureSuccess();

        new DotNet()
            .WithPathToApplication(Path.Combine(tempDirectory, "MyApp.dll"))
            .Run().EnsureSuccess();
        // }
    }
}