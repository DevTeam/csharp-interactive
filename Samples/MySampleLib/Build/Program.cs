﻿using HostApi;

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
    .WithShortName("Tests")
    .WithNoBuild(true)
    .WithConfiguration(configuration)
    .Build();

if (testResult.ExitCode != 0)
{
    Error("Tests failed.");
    return 1;
}

return 0;