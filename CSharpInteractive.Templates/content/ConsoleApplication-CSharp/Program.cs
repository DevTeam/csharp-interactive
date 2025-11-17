using HostApi;

// Build a dotnet solution or project
new DotNetBuild().WithConfiguration("Release").Build().EnsureSuccess();