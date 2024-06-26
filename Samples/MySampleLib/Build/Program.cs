using HostApi;

if (!Props.TryGetValue("configuration", out var configuration))
{
    configuration = "Release";
}

Info($"Configuration: {configuration}");

var buildResult = new DotNetBuild()
    .WithShortName("Build")
    .WithConfiguration(configuration)
    .WithProject("MySampleLib.sln")
    .Build();

if (buildResult.ExitCode != 0)
{
    Error("Build failed.");
    return 1;
}

var testResult = new DotNetTest()
    .WithShortName("Test")
    .WithNoBuild(true)
    .WithConfiguration(configuration)
    .Build(i =>
    {
        if (i.TestResult is { } testResult)
        {
            switch (testResult.State)
            {
                case TestState.Passed:
                    Info($"{testResult.FullyQualifiedName} - OK");
                    break;
                
                case TestState.Failed:
                    Error($"{testResult.FullyQualifiedName} - Failed");
                    break;
            }
        }
    });

if (testResult.ExitCode != 0)
{
    Error("Test failed.");
    return 1;
}

return 0;