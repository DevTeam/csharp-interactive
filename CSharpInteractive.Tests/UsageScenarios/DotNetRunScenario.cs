// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetRunScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("console")
            .WithName("MyApp")
            .WithForce(true)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Running source code without any explicit compile or launch commands
        // {
        // ## using HostApi;

        var stdOut = new List<string>();
        new DotNetRun()
            .WithProject(Path.Combine("MyApp", "MyApp.csproj"))
            .Build(message => stdOut.Add(message.Text))
            .EnsureSuccess();

        // Checks stdOut
        stdOut.ShouldBe(new[] {"Hello, World!"});
        // }
    }
}