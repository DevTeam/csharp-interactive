// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
var result = new DotNetNew()
    .WithTemplateName("classlib")
    .WithName("MyLib")
    .WithForce(true)
    .Build().EnsureSuccess();

result.ExitCode.ShouldBe(0, result.ToString());

// Builds the library project, running a command like: "dotnet msbuild /t:Build -restore /p:configuration=Release -verbosity=detailed" from the directory "MyLib"
result = new MSBuild()
    .WithWorkingDirectory("MyLib")
    .WithTarget("Build")
    .WithRestore(true)
    .AddProps(("configuration", "Release"))
    .WithVerbosity(DotNetVerbosity.Detailed)
    .Build().EnsureSuccess();

// The "result" variable provides details about a build
result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse(result.ToString());
result.ExitCode.ShouldBe(0, result.ToString());
