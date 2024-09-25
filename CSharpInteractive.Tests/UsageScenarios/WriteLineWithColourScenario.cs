// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

using static HostApi.Color;

public class WriteLineWithColourScenario(ITestOutputHelper output) : BaseScenario(output)
{

    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=01 Output, logging and tracing
        // $priority=01
        // $description=Write a line highlighted with "Header" color to a build log
        // {
        WriteLine("Hello", Header);
        // }
    }
}