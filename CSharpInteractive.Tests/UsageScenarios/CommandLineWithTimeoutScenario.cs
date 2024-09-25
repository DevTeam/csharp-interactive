// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

using System;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class CommandLineWithTimeoutScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(Environment.OSVersion.Platform == PlatformID.Win32NT);
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));
        ExpectedExitCode = 1;

        // $visible=true
        // $tag=05 Command Line
        // $priority=06
        // $description=Run timeout
        // $header=If timeout expired a process will be killed.
        // {
        // ## using HostApi;

        var exitCode = new CommandLine("cmd", "/c", "TIMEOUT", "/T", "120")
            .Run(default, TimeSpan.FromMilliseconds(1))
            .ExitCode;
        // }

        exitCode.HasValue.ShouldBeFalse(exitCode.ToString());
    }
}