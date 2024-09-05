// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace CSharpInteractive.Tests.UsageScenarios;

public class WriteEmptyLineScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=01 Output, logging and tracing
        // $priority=01
        // $description=Write an empty line to a build log
        // {
        WriteLine();
        // }
    }
}