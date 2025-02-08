// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

using System;

public class LogErrorScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=01 Output, logging and tracing
        // $priority=02
        // $description=Registering errors in the build log
        // {
        Error("Error info");
        Error("Error info", "Error identifier");
        Error("Error: ".WithColor(), "datails".WithColor(Color.Details));
        // }
    }
}