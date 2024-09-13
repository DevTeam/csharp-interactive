// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

using System;
using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
public class CommandLineOutputScenario : BaseScenario
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(Environment.OSVersion.Platform == PlatformID.Win32NT);
        Skip.IfNot(string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")));

        // $visible=true
        // $tag=05 Command Line
        // $priority=04
        // $description=Run and process output
        // {
        // Adds the namespace "HostApi" to use Command Line API
        // ## using HostApi;

        var lines = new List<string>();
        var result = new CommandLine("cmd", "/c", "SET")
            .AddVars(("MyEnv", "MyVal"))
            .Run(output => lines.Add(output.Line))
            .EnsureSuccess();

        lines.ShouldContain("MyEnv=MyVal");
        // }

        result.ExitCode.HasValue.ShouldBeTrue();
    }
}