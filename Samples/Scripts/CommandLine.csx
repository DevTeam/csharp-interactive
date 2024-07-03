//#l diagnostic
using HostApi;

var exitCode = new CommandLine("whoami.exe", "/all").Run();
Info(exitCode?.ToString() ?? "empty");