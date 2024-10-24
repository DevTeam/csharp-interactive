// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
var result = new DotNetNew()
    .WithTemplateName("classlib")
    .AddArgs("-f", "net8.0")
    .WithName("MyLib")
    .WithForce(true)
    .Build().EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Publish the project, running a command like: "dotnet publish --framework net6.0" from the directory "MyLib"
result = new DotNetPublish()
    .WithWorkingDirectory("MyLib")
    .WithFramework("net8.0")
    .Build().EnsureSuccess();
