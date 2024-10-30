// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetCsiScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=02
        // $description=Running C# script
        // {
        // ## using HostApi;

        var script = Path.GetTempFileName();
        File.WriteAllText(script, "Console.WriteLine($\"Hello, {Args[0]}!\");");

        var stdOut = new List<string>();
        new DotNetCsi()
            .WithScript(script)
            .AddArgs("World")
            .Run(output => stdOut.Add(output.Line))
            .EnsureSuccess();

        // Checks stdOut
        stdOut.Contains("Hello, World!").ShouldBeTrue();
        // }
    }
}