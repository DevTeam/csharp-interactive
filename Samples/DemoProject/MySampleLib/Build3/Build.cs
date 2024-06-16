using HostApi;
// ReSharper disable CheckNamespace
// ReSharper disable ArrangeTypeModifiers

interface IBuild
{
    Task<string> BuildAsync();
}

class Build(
    Settings settings,
    IBuildRunner runner) : IBuild
{
    public async Task<string> BuildAsync()
    {
        var build = new DotNetBuild()
            .WithConfiguration(settings.Configuration)
            .AddProps(("version", settings.Version.ToString()));

        await Assertion.Succeed(runner.RunAsync(build));

        var test = new DotNetTest()
            .WithConfiguration(settings.Configuration)
            .WithNoBuild(true);

        var testInContainer = new DockerRun(
                test.WithExecutablePath("dotnet"),
                "mcr.microsoft.com/dotnet/sdk:6.0")
            .WithPlatform("linux")
            .AddVolumes((Environment.CurrentDirectory, "/project"))
            .WithContainerWorkingDirectory("/project");

        await Assertion.Succeed(
            Task.WhenAll(
                runner.RunAsync(test),
                runner.RunAsync(testInContainer)
            )
        );

        var output = Path.Combine("bin", settings.Configuration, "output");

        var publish = new DotNetPublish()
            .WithWorkingDirectory("BlazorServerApp")
            .WithConfiguration(settings.Configuration)
            .WithNoBuild(true)
            .WithOutput(output)
            .AddProps(("version", settings.Version.ToString()));

        await Assertion.Succeed(runner.RunAsync(publish));
        return Path.Combine("BlazorServerApp", output);
    }
}