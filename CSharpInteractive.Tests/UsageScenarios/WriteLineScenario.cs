// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

public class WriteLineScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=01 Output, logging and tracing
        // $priority=00
        // $description=Writing a line to a build log
        // {
        WriteLine("Hello");
        // }
    }
}