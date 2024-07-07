// ReSharper disable UnusedMember.Global
namespace HostApi;

public interface ICommandLineRunner
{
    ICommandLineResult Run(
        ICommandLine commandLine,
        Action<Output>? handler = default,
        TimeSpan timeout = default);

    Task<ICommandLineResult> RunAsync(
        ICommandLine commandLine,
        Action<Output>? handler = default,
        CancellationToken cancellationToken = default);
}