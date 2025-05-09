// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantAssignment

namespace CSharpInteractive.Tests.UsageScenarios;

using System;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class CommandLinesScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        Skip.IfNot(Environment.OSVersion.Platform == PlatformID.Win32NT);

        // $visible=true
        // $tag=05 Command Line
        // $priority=00
        // $description=Building custom command lines
        // {
        // ## using HostApi;

        // Creates and run a simple command line 
        "whoami".AsCommandLine().Run().EnsureSuccess();

        // Creates and run a simple command line 
        new CommandLine("whoami").Run().EnsureSuccess();

        // Creates and run a command line with arguments 
        new CommandLine("cmd", "/c", "echo", "Hello").Run();

        // Same as previous statement
        new CommandLine("cmd", "/c")
            .AddArgs("echo", "Hello")
            .Run().EnsureSuccess();

        (new CommandLine("cmd") + "/c" + "echo" + "Hello")
            .Run().EnsureSuccess();

        "cmd".AsCommandLine("/c", "echo", "Hello")
            .Run().EnsureSuccess();

        ("cmd".AsCommandLine() + "/c" + "echo" + "Hello")
            .Run().EnsureSuccess();

        // Just builds a command line with multiple environment variables
        var cmd = new CommandLine("cmd", "/c", "echo", "Hello")
            .AddVars(("Var1", "val1"), ("var2", "Val2"));

        // Same as previous statement
        cmd = new CommandLine("cmd") + "/c" + "echo" + "Hello" + ("Var1", "val1") + ("var2", "Val2");

        // Builds a command line to run from a specific working directory 
        cmd = new CommandLine("cmd", "/c", "echo", "Hello")
            .WithWorkingDirectory("MyDyrectory");

        // Builds a command line and replaces all command line arguments
        cmd = new CommandLine("cmd", "/c", "echo", "Hello")
            .WithArgs("/c", "echo", "Hello !!!");
        // }
    }
}