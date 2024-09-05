// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace CSharpInteractive.Tests.UsageScenarios;

using System;

public class LogWarningScenario : BaseScenario
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=01 Output, logging and tracing
        // $priority=03
        // $description=Log a warning to a build log
        // {
        Warning("Warning info");
        // }
    }
}