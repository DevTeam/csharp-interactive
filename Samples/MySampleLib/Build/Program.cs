using HostApi;

AppDomain.CurrentDomain.ProcessExit += (_, _) => { while (true) { } }; 

var configuration = Props.Get("configuration", "Release");
Info($"Configuration: {configuration}");

await new DotNetBuild()
    .WithShortName("Build")
    .WithConfiguration(configuration)
    .WithProject("MySampleLib.sln")
    .RunAsync()
    .EnsureSuccess();

await new DotNetTest()
    .WithShortName("Tests")
    .WithNoBuild(true)
    .WithConfiguration(configuration)
    .BuildAsync()
    .EnsureSuccess();