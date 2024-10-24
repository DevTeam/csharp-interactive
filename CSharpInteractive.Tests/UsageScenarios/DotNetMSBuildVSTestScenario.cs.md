// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new test project, running a command like: "dotnet new mstest -n MyTests --force"
var result = new DotNetNew()
    .WithTemplateName("mstest")
    .WithName("MyTests")
    .WithForce(true)
    .Build().EnsureSuccess();

result.ExitCode.ShouldBe(0, result.ToString());

// Runs tests via a command like: "dotnet msbuild /t:VSTest" from the directory "MyTests"
result = new MSBuild()
    .WithTarget("VSTest")
    .WithWorkingDirectory("MyTests")
    .Build().EnsureSuccess();

// The "result" variable provides details about a build
result.ExitCode.ShouldBe(0, result.ToString());
result.Summary.Tests.ShouldBe(1, result.ToString());
result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1, result.ToString());
