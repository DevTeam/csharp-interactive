// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new console project, running a command like: "dotnet new console -n MyApp --force"
new DotNetNew()
    .WithTemplateName("console")
    .WithName("MyApp")
    .WithForce(true)
    .Build().EnsureSuccess();

var tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
new DotNetPublish()
    .WithWorkingDirectory("MyApp")
    .WithOutput(tempDirectory)
    .Build().EnsureSuccess();

new DotNet()
    .WithPathToApplication(Path.Combine(tempDirectory, "MyApp.dll"))
    .Run().EnsureSuccess();
