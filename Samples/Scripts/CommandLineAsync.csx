#l diagnostic
using HostApi;

var task1 = new CommandLine("whoami.exe", "/all").RunAsync();
var task2 = new CommandLine("whoami.exe", "/all").RunAsync();
Task.WaitAll([task1, task2]);
