// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

public class PropsScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=02 Arguments and parameters
        // $priority=01
        // $description=Using properties
        // {
        WriteLine(Props["version"]);
        WriteLine(Props.Get("configuration", "Release"));

        // Some CI/CDs have integration of these properties.
        // For example in TeamCity this property with all changes will be available in the next TeamCity steps.
        Props["version"] = "1.1.6";
        // }
    }
}