// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_Elsewhere
namespace CSharpInteractive.Tests.UsageScenarios;

using System;
using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
public class CommandLineAsyncCancellation : BaseScenario
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(Environment.OSVersion.Platform == PlatformID.Win32NT);
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=05 Command Line
        // $priority=06
        // $description=Cancellation of asynchronous run
        // $header=Cancellation will destroy the process and its child processes.
        // {
        // Adds the namespace "HostApi" to use Command Line API
        // ## using HostApi;

        var cancellationTokenSource = new CancellationTokenSource();
        var task = new CommandLine("cmd", "/c", "TIMEOUT", "/T", "120")
            .RunAsync(default, cancellationTokenSource.Token);

        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(100));
        task.IsCompleted.ShouldBeFalse();
        // }
    }
}