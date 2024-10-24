// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;
using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetCsiScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=02
        // $description=Run C# script
        // {
        // Adds the namespace "HostApi" to use .NET build API
        // ## using HostApi;

        var script = Path.GetTempFileName();
        File.WriteAllText(script, "Console.WriteLine($\"Hello, {Args[0]}!\");");

        var stdOut = new List<string>();
        var result = new DotNetCsi()
            .WithScript(script)
            .AddArgs("World")
            .Build(message => stdOut.Add(message.Text))
            .EnsureSuccess();

        result.ExitCode.ShouldBe(0);

        // Checks StdOut
        stdOut.Contains("Hello, World!").ShouldBeTrue(result.ToString());
        // }
    }
}