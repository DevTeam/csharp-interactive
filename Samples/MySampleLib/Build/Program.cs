using HostApi;

var configuration = Props.Get("configuration", "Release");
Info($"Configuration: {configuration}");

await new DotNetBuild()
    .WithShortName("Solution build")
    .WithConfiguration(configuration)
    .WithNoLogo(true)
    .WithVerbosity(DotNetVerbosity.Quiet)
    .RunAsync()
    .EnsureSuccess();

await new DotNetTest()
    .WithShortName("Tests")
    .WithNoLogo(true)
    .WithVerbosity(DotNetVerbosity.Quiet)
    .WithNoBuild(true)
    .WithConfiguration(configuration)
    .BuildAsync()
    .EnsureSuccess();