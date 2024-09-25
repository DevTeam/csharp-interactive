// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetExecScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // Creates a new console project
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
        // $description=Execute a dotnet application
        // {
        // ## using HostApi;
        new DotNetExec()
            .WithPathToApplication(Path.Combine(path, "MyApp.dll"))
            .Run().EnsureSuccess();
        // }
    }
}