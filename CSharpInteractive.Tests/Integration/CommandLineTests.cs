// ReSharper disable StringLiteralTypo

namespace CSharpInteractive.Tests.Integration;

using System.Diagnostics;
using Core;
using HostApi;
using Microsoft.Extensions.DependencyInjection;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
public class CommandLineTests
{
    [Fact]
    public void ShouldRun()
    {
        // Given
        var events = new List<Output>();

        // When
        var result = GetService<ICommandLineRunner>().Run(DotNetScript.Create("WriteLine(\"Hello\");"), e => events.Add(e));

        // Then
        var exitCode = result.ExitCode;
        exitCode.HasValue.ShouldBeTrue();
        exitCode.ShouldBe(0);
        events.Any(i => i.IsError).ShouldBeFalse();
        events.Any(i => !i.IsError && i.Line.Contains("Hello")).ShouldBeTrue();
    }

    [Fact]
    public void ShouldRunWithEnvironmentVariable()
    {
        // Given
        var events = new List<Output>();

        // When
        var result = GetService<ICommandLineRunner>().Run(
            DotNetScript.Create("WriteLine(\"VAL=\" + System.Environment.GetEnvironmentVariable(\"ABC\"));").AddVars(("ABC", "123")),
            e => events.Add(e));

        // Then
        var exitCode = result.ExitCode;
        exitCode.HasValue.ShouldBeTrue();
        exitCode.ShouldBe(0);
        events.Any(i => i.IsError).ShouldBeFalse();
        events.Any(i => !i.IsError && i.Line.Contains("VAL=123")).ShouldBeTrue();
    }

    [Fact]
    public void ShouldRunUsingTimeout()
    {
        // Given
        var stopwatch = new Stopwatch();

        // When
        stopwatch.Start();
        var result = GetService<ICommandLineRunner>().Run(
            DotNetScript.Create("System.Threading.Thread.Sleep(TimeSpan.FromSeconds(15));"),
            _ => { },
            TimeSpan.FromMilliseconds(100));
        stopwatch.Stop();

        // Then
        var exitCode = result.ExitCode;
        exitCode.HasValue.ShouldBeFalse();
        stopwatch.ElapsedMilliseconds.ShouldBeLessThanOrEqualTo(10000);
        stopwatch.ElapsedMilliseconds.ShouldBeGreaterThanOrEqualTo(100);
    }

    [Fact]
    public async Task ShouldRunAsync()
    {
        // Given
        var events = new List<Output>();

        // When
        var result = await GetService<ICommandLineRunner>().RunAsync(DotNetScript.Create("WriteLine(\"Hello\");"), e => events.Add(e));

        // Then
        var exitCode = result.ExitCode;
        exitCode.HasValue.ShouldBeTrue();
        exitCode.ShouldBe(0);
        events.Any(i => i.IsError).ShouldBeFalse();
        events.Any(i => !i.IsError && i.Line.Contains("Hello")).ShouldBeTrue();
    }

    [Fact]
    public void ShouldRunAsyncUsingCancellationToken()
    {
        // Given
        using var cancellationTokenSource = new CancellationTokenSource();
        var stopwatch = new Stopwatch();

        // When
        stopwatch.Start();
        GetService<ICommandLineRunner>().RunAsync(
            DotNetScript.Create("System.Threading.Thread.Sleep(TimeSpan.FromSeconds(15));"),
            _ => { },
            cancellationTokenSource.Token);

        Thread.Sleep(100);
        cancellationTokenSource.Cancel();
        stopwatch.Stop();

        // Then
        stopwatch.ElapsedMilliseconds.ShouldBeLessThanOrEqualTo(10000);
        stopwatch.ElapsedMilliseconds.ShouldBeGreaterThanOrEqualTo(100);
    }

    private static T GetService<T>() => Composition.Shared.Root.GetService<T>()!;
}