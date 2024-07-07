using HostApi;

var configuration = Props.Get("configuration", "Release");
Info($"Configuration: {configuration}");

await new DotNetBuild()
    .WithShortName("Build")
    .WithConfiguration(configuration)
    .WithProject("MySampleLib.sln")
    .RunAsync()
    .EnsureSuccess();

await EunTestsAsync();

async Task EunTestsAsync()
{
    await Task.Delay(100);
    
    await new DotNetTest()
        .WithShortName("Tests")
        .WithNoBuild(true)
        .WithConfiguration(configuration)
        .BuildAsync()
        .EnsureSuccess();

    await Task.Delay(100);
}