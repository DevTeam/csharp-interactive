using HostApi;

var configuration = Props.Get("configuration", "Release");
Info($"Configuration: {configuration}");

new DotNetBuild()
    .WithConfiguration(configuration)
    .WithNoLogo(true)
    .Build()
    .EnsureSuccess();

var cts = new CancellationTokenSource();
await new DotNetTest()
    .WithConfiguration(configuration)
    .WithNoLogo(true)
    .WithNoBuild(true)
    .BuildAsync(i =>
    {
        if (i.TestResult is { State: TestState.Failed })
        {
            // cts.Cancel();
        }
    }, cts.Token)
    .EnsureSuccess();