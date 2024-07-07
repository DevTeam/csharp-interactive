using HostApi;

var configuration = Props.Get("configuration", "Release");
Info($"Configuration: {configuration}");

await new DotNetBuild()
    .WithShortName("Build")
    .WithConfiguration(configuration)
    .WithProject("MySampleLib.sln")
    .RunAsync()
    .EnsureSuccess();

new DotNetTest()
    .WithShortName("Tests")
    .WithNoBuild(true)
    .WithConfiguration(configuration)
    .Build()
    .EnsureSuccess();