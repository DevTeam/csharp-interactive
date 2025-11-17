// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetSlnAddScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();
        
        new DotNetNew()
            .WithTemplateName("xunit")
            .WithName("MyTests")
            .WithForce(true)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Adding projects to the solution file
        // {
        // ## using HostApi;

        new DotNetNew()
            .WithTemplateName("sln")
            .WithName("MySolution")
            .WithForce(true)
            .AddArgs("--format", "sln")
            .Run().EnsureSuccess();

        new CommandLine("dir").Run();

        new DotNetSlnAdd()
            .WithSolution("MySolution.sln")
            .AddProjects(
                Path.Combine("MyLib", "MyLib.csproj"),
                Path.Combine("MyTests", "MyTests.csproj"))
            .Run().EnsureSuccess();
        // }

        var lines = new List<string>();
        new DotNetSlnList()
            .WithSolution("MySolution.sln")
            .Run(output => lines.Add(output.Line))
            .EnsureSuccess();
        
        lines.Count.ShouldBe(4);
    }
}