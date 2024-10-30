// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable RedundantAssignment

namespace CSharpInteractive.Tests.UsageScenarios;

using System;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class CommandLineAsyncScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public async Task Run()
    {
        Skip.IfNot(Environment.OSVersion.Platform == PlatformID.Win32NT);
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=05 Command Line
        // $priority=02
        // $description=Running a command line asynchronously
        // {
        // ## using HostApi;

        await GetService<ICommandLineRunner>()
            .RunAsync(new CommandLine("cmd", "/C", "DIR")).EnsureSuccess();

        // or the same thing using the extension method
        var result = await new CommandLine("cmd", "/c", "DIR")
            .RunAsync().EnsureSuccess();
        // }

        result.ExitCode.HasValue.ShouldBeTrue(result.ToString());
    }
}