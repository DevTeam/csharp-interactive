// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new console project, running a command like: "dotnet new console -n MyApp --force"
var result = new DotNetNew()
    .WithTemplateName("console")
    .WithName("MyApp")
    .WithForce(true)
    .Build().EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Runs the console project using a command like: "dotnet run" from the directory "MyApp"
var stdOut = new List<string>();
result = new DotNetRun()
    .WithWorkingDirectory("MyApp")
    .Build(message => stdOut.Add(message.Text))
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Checks StdOut
stdOut.ShouldBe(new[] {"Hello, World!"});
