// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_Elsewhere

namespace CSharpInteractive.Tests.UsageScenarios;

using System;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class CommandLineInParallelScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public async Task Run()
    {
        Skip.IfNot(Environment.OSVersion.Platform == PlatformID.Win32NT);
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=05 Command Line
        // $priority=05
        // $description=Run asynchronously in parallel
        // {
        // ## using HostApi;

        var task = new CommandLine("cmd", "/c", "DIR")
            .RunAsync().EnsureSuccess();

        var result = new CommandLine("cmd", "/c", "SET")
            .Run().EnsureSuccess();

        await task;
        // }

        task.Result.ExitCode.HasValue.ShouldBeTrue(result.ToString());
        result.ExitCode.HasValue.ShouldBeTrue(result.ToString());
    }
}