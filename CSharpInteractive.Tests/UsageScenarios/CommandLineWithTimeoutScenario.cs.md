// Adds the namespace "HostApi" to use Command Line API
using HostApi;

int? exitCode = new CommandLine("cmd", "/c", "TIMEOUT", "/T", "120")
    .Run(default, TimeSpan.FromMilliseconds(1))
    .EnsureSuccess()
    .ExitCode;
