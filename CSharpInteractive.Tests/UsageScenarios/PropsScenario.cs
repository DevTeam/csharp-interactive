// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace CSharpInteractive.Tests.UsageScenarios;

public class PropsScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=08 Global state
        // $priority=01
        // $description=Using Props
        // {
        WriteLine(Props["version"]);
        WriteLine(Props.Get("configuration", "Release"));

        // Some CI/CDs have integration of these properties.
        // For example in TeamCity this property with all changes will be available in the next TeamCity steps.
        Props["version"] = "1.1.6";
        // }
    }
}