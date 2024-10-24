// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
var result = new DotNetNew()
    .WithTemplateName("classlib")
    .WithName("MyLib")
    .WithForce(true)
    .Build().EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Creates a NuGet package of version 1.2.3 for the project, running a command like: "dotnet pack /p:version=1.2.3" from the directory "MyLib"
result = new DotNetPack()
    .WithWorkingDirectory("MyLib")
    .AddProps(("version", "1.2.3"))
    .Build().EnsureSuccess();
