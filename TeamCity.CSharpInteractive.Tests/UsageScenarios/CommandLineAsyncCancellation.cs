// ReSharper disable StringLiteralTypo
namespace TeamCity.CSharpInteractive.Tests.UsageScenarios
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Cmd;
    using Shouldly;
    using Xunit;

    public class CommandLineAsyncCancellation: Scenario
    {
        [SkippableFact]
        public void Run()
        {
            Skip.IfNot(Environment.OSVersion.Platform == PlatformID.Win32NT);
            Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

            // $visible=true
            // $tag=10 Command Line API
            // $priority=06
            // $description=Cancellation of asynchronous run
            // $header=The cancellation will kill a related process.
            // {
            // Adds the namespace "Cmd" to use ICommandLine
            // ## using Cmd;

            var cancellationTokenSource = new CancellationTokenSource();
            Task<int?> task = GetService<ICommandLine>().RunAsync(
                new CommandLine("cmd", "/c", "TIMEOUT", "/T", "120"),
                default,
                cancellationTokenSource.Token);
            
            cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(100));
            task.IsCompleted.ShouldBeFalse();
            // }
        }
    }
}