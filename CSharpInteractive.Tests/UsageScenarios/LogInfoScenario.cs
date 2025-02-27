// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

using System;

public class LogInfoScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=01 Output, logging and tracing
        // $priority=04
        // $description=Registering information in the build log
        // {
        Info("Some info");
        Info("Some ", "info".WithColor(Color.Details));
        // }
    }
}