// Adds the namespace "HostApi" to use Command Line API and Docker API
using HostApi;

// Creates some command line to run in a docker container
var cmd = new CommandLine("whoami");

// Runs the command line in a docker container
var result = new DockerRun(cmd, "mcr.microsoft.com/dotnet/sdk")
    .WithAutoRemove(true)
    .Run()
    .EnsureSuccess();
