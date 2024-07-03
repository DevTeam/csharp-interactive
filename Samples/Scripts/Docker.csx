using HostApi;

var cmd = new CommandLine("whoami");
var dockerCmd = new DockerRun(cmd, "ubuntu");
dockerCmd.Run();