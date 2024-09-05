// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo
namespace CSharpInteractive.Tests.UsageScenarios;

using HostApi;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
public class MSBuildScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Build a project using MSBuild
        // {
        // Adds the namespace "HostApi" to use .NET build API
        // ## using HostApi;

        // Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
        var result = new DotNetNew("classlib", "-n", "MyLib", "--force")
            .Build()
            .EnsureSuccess();
        
        result.ExitCode.ShouldBe(0);

        // Builds the library project, running a command like: "dotnet msbuild /t:Build -restore /p:configuration=Release -verbosity=detailed" from the directory "MyLib"
        result = new MSBuild()
            .WithWorkingDirectory("MyLib")
            .WithTarget("Build")
            .WithRestore(true)
            .AddProps(("configuration", "Release"))
            .WithVerbosity(DotNetVerbosity.Detailed)
            .Build()
            .EnsureSuccess();

        // The "result" variable provides details about a build
        result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse();
        result.ExitCode.ShouldBe(0);
        // }
    }
}