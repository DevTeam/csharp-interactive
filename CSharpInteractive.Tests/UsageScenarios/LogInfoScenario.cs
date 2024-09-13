// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

using System;

public class LogInfoScenario : BaseScenario
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=01 Output, logging and tracing
        // $priority=04
        // $description=Log information to a build log
        // {
        Info("Some info");
        // }
    }
}