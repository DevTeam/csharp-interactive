// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace CSharpInteractive.Tests.UsageScenarios;

using System;
using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
public class CommandLineWithTimeoutScenario : BaseScenario
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(Environment.OSVersion.Platform == PlatformID.Win32NT);
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=10 Command Line API
        // $priority=06
        // $description=Run timeout
        // $header=If timeout expired a process will be killed.
        // {
        // Adds the namespace "HostApi" to use Command Line API
        // ## using HostApi;

        int? exitCode = new CommandLine("cmd", "/c", "TIMEOUT", "/T", "120").Run(default, TimeSpan.FromMilliseconds(1)).ExitCode;

        exitCode.HasValue.ShouldBeFalse();
        // }
    }
}