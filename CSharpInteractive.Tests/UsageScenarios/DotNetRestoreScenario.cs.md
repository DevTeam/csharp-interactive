// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
var result = new DotNetNew()
    .WithTemplateName("classlib")
    .WithName("MyLib")
    .WithForce(true)
    .Build().EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Restore the project, running a command like: "dotnet restore" from the directory "MyLib"
result = new DotNetRestore()
    .WithWorkingDirectory("MyLib")
    .Build().EnsureSuccess();
