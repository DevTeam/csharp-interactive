// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

using System;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class CommandLineOutputScenario(ITestOutputHelper output) : BaseScenario(output)
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
        // ## using HostApi;

        var lines = new List<string>();
        var result = new CommandLine("cmd", "/c", "SET")
            .AddVars(("MyEnv", "MyVal"))
            .Run(output => lines.Add(output.Line)).EnsureSuccess();

        lines.ShouldContain("MyEnv=MyVal");
        // }

        result.ExitCode.HasValue.ShouldBeTrue(result.ToString());
    }
}