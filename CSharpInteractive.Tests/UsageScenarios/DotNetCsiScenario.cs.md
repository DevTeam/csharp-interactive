// Adds the namespace "HostApi" to use .NET build API
using HostApi;

var script = Path.GetTempFileName();
File.WriteAllText(script, "Console.WriteLine($\"Hello, {Args[0]}!\");");

var stdOut = new List<string>();
var result = new DotNetCsi()
    .WithScript(script)
    .AddArgs("World")
    .Build(message => stdOut.Add(message.Text))
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Checks StdOut
stdOut.Contains("Hello, World!").ShouldBeTrue(result.ToString());
