// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetSlnRemoveScenario(ITestOutputHelper output) : BaseScenario(output)
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
        
        new DotNetNew()
            .WithTemplateName("sln")
            .WithName("MySolution")
            .WithForce(true)
            .AddArgs("--format", "sln")
            .Run().EnsureSuccess();

        new DotNetSlnAdd()
            .WithSolution("MySolution.sln")
            .AddProjects(
                Path.Combine("MyLib", "MyLib.csproj"),
                Path.Combine("MyTests", "MyTests.csproj"))
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Removing a project or multiple projects from the solution file
        // {
        // ## using HostApi;

        new DotNetSlnRemove()
            .WithSolution("MySolution.sln")
            .AddProjects(
                Path.Combine("MyLib", "MyLib.csproj"))
            .Run().EnsureSuccess();
        // }

        var lines = new List<string>();
        new DotNetSlnList()
            .WithSolution("MySolution.sln")
            .Run(output => lines.Add(output.Line))
            .EnsureSuccess();
        
        lines.Count.ShouldBe(3);
    }
}