// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetToolRunScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("tool-manifest")
            .Run().EnsureSuccess();

        new DotNetToolInstall()
            .WithLocal(true)
            .WithPackage("dotnet-csi")
            .WithVersion("1.1.1")
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Invoking a local tool
        // {
        // ## using HostApi;

        var script = Path.GetTempFileName();
        File.WriteAllText(script, "Console.WriteLine($\"Hello, {Args[0]}!\");");

        var stdOut = new List<string>();
        new DotNetToolRun()
            .WithCommandName("dotnet-csi")
            .AddArgs(script)
            .AddArgs("World")
            .Run(output => stdOut.Add(output.Line))
            .EnsureSuccess();
        
        // Checks stdOut
        stdOut.Contains("Hello, World!").ShouldBeTrue();
        // }
    }
}