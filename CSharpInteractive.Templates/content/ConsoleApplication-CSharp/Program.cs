using HostApi;

// Builds a dotnet solution or project
return new DotNetBuild().Build().ExitCode ?? 1;