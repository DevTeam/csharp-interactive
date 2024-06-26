// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace CSharpInteractive.Tests.UsageScenarios;

using System;

public class LogTraceScenario : BaseScenario
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=09 Logging
        // $priority=05
        // $description=Log trace information to a build log
        // {
        Trace("Some trace info");
        // }
    }
}