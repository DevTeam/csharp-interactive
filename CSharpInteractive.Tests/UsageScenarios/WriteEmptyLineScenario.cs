// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

public class WriteEmptyLineScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=01 Output, logging and tracing
        // $priority=01
        // $description=Writing an empty line to a build log
        // {
        WriteLine();
        // }
    }
}