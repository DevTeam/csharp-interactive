// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetRemoveReferenceScenario(ITestOutputHelper output) : BaseScenario(output)
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
        // $description=Adding a project reference
        // {
        // ## using HostApi;

        new DotNetAddReference()
            .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
            .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
            .Run().EnsureSuccess();
        
        new DotNetRemoveReference()
            .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
            .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
            .Run().EnsureSuccess();
        
        var lines = new List<string>();
        new DotNetListReference()
            .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
            .Run(output => lines.Add(output.Line));

        lines.Any(i => i.Contains("MyLib.csproj")).ShouldBeFalse();
        // }
    }
}