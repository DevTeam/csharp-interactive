// ReSharper disable UnusedMemberInSuper.Global
namespace HostApi;

public interface IBuildResult: ICommandLineResult
{
    IReadOnlyList<BuildMessage> Errors { get; }

    IReadOnlyList<BuildMessage> Warnings { get; }

    IReadOnlyList<TestResult> Tests { get; }

    BuildStatistics Summary { get; }
}