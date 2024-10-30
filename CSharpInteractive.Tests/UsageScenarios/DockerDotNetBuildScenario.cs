// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable SeparateLocalFunctionsWithJumpStatement

// ReSharper disable MoveLocalFunctionAfterJumpStatement
namespace CSharpInteractive.Tests.UsageScenarios;

using System;
using System.Diagnostics.CodeAnalysis;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[Trait("Docker", "True")]
public class DockerDotNetBuildScenario(ITestOutputHelper output) : BaseScenario(output)
{
    //[Fact(Skip = "Linux Docker only")]
    [SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped")]
    [Fact(Skip = "")]
    public void Run()
    {
        // $visible=true
        // $tag=06 Docker CLI
        // $priority=01
        // $description=Building a project in a docker container
        // {
        // ## using HostApi;

        // Creates a base docker command line
        var dockerRun = new DockerRun()
            .WithAutoRemove(true)
            .WithInteractive(true)
            .WithImage("mcr.microsoft.com/dotnet/sdk")
            .WithPlatform("linux")
            .WithContainerWorkingDirectory("/MyProjects")
            .AddVolumes((ToAbsoluteLinuxPath(Environment.CurrentDirectory), "/MyProjects"));

        // Creates a new library project in a docker container
        dockerRun
            .WithCommandLine(new DotNetCustom("new", "classlib", "-n", "MyLib", "--force"))
            .Run().EnsureSuccess();

        // Builds the library project in a docker container
        var result = dockerRun
            .WithCommandLine(new DotNetBuild().WithProject("MyLib/MyLib.csproj"))
            .Build().EnsureSuccess();

        string ToAbsoluteLinuxPath(string path) =>
            "/" + path.Replace(":", "").Replace('\\', '/');
        // }
        
        // The "result" variable provides details about a build
        result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse(result.ToString());
    }
}