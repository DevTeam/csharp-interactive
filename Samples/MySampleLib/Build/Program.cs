using HostApi;

var configuration = Props.Get("configuration", "Release");
Info($"Configuration: {configuration}");

new DotNetBuild()
    .WithShortName("Solution build")
    .WithConfiguration(configuration)
    .WithNoLogo(true)
    .WithVerbosity(DotNetVerbosity.Quiet)
    .Build()
    .EnsureSuccess();

using var cts = new CancellationTokenSource();
await new DotNetTest()
    .WithShortName("Tests")
    .WithNoLogo(true)
    .WithVerbosity(DotNetVerbosity.Quiet)
    .WithNoBuild(true)
    .WithConfiguration(configuration)
    .BuildAsync(i =>
    {
        if (i.TestResult is { State: TestState.Failed } testResult)
        {
            Error($"{testResult.FullyQualifiedName} - failed");
            cts.Cancel();
        }
    }, cts.Token)
    .EnsureSuccess();