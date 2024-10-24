// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
new DotNetNew()
    .WithTemplateName("xunit")
    .WithName("MyLib")
    .WithForce(true)
    .Build().EnsureSuccess();

// Builds the library project, running a command like: "dotnet build" from the directory "MyLib"
var messages = new List<BuildMessage>();
var result = new DotNetBuild()
    .WithWorkingDirectory("MyLib")
    .Build(message => messages.Add(message)).EnsureSuccess();

// The "result" variable provides details about a build
messages.Count.ShouldBeGreaterThan(0, result.ToString());
result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse();
result.ExitCode.ShouldBe(0);

// Runs tests in docker
result = new DotNetTest()
    .WithWorkingDirectory("MyLib")
    .Build()
    .EnsureSuccess();
