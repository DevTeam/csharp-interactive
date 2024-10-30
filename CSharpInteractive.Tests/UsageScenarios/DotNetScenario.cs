// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("console")
            .WithName("MyApp")
            .WithForce(true)
            .Run().EnsureSuccess();

        var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        new DotNetPublish()
            .WithWorkingDirectory("MyApp")
            .WithOutput(path)
            .Build().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Running a .NET application
        // {
        // Adds the namespace "HostApi" to use .NET build API
        // ## using HostApi;

        new DotNet()
            .WithPathToApplication(Path.Combine(path, "MyApp.dll"))
            .Run().EnsureSuccess();
        // }
    }
}