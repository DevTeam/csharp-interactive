// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetListPackageScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Printing NuGet packages for a project
        // {
        // ## using HostApi;
        
        new DotNetAddPackage()
            .WithWorkingDirectory("MyLib")
            .WithPackage("Pure.DI")
            .Run().EnsureSuccess();

        var lines = new List<string>();
        new DotNetListPackage()
            .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
            .WithVerbosity(DotNetVerbosity.Minimal)
            .Run(output => lines.Add(output.Line));
        
        lines.Any(i => i.Contains("Pure.DI")).ShouldBeTrue();
        // }
    }
}