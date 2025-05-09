// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

using System;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class CommandLineScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(Environment.OSVersion.Platform == PlatformID.Win32NT);
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=05 Command Line
        // $priority=01
        // $description=Running a command line
        // {
        // ## using HostApi;

        GetService<ICommandLineRunner>()
            .Run(new CommandLine("cmd", "/c", "DIR")).EnsureSuccess();

        // or the same thing using the extension method
        new CommandLine("cmd", "/c", "DIR")
            .Run().EnsureSuccess();

        // using operator '+'
        var cmd = new CommandLine("cmd") + "/c" + "DIR";
        cmd.Run().EnsureSuccess();

        // with environment variables
        cmd = new CommandLine("cmd") + "/c" + "DIR" + ("MyEnvVar", "Some Value");
        cmd.Run().EnsureSuccess();
        // }
    }
}