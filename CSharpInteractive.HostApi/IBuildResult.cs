// ReSharper disable UnusedMemberInSuper.Global
namespace HostApi;

/// <summary>
/// Provides information about a build.
/// </summary>
/// <seealso cref="IBuildRunner.Build"/>
/// <seealso cref="IBuildRunner.BuildAsync"/>
public interface IBuildResult: ICommandLineResult
{
    /// <summary>
    /// List of build errors.
    /// </summary>
    IReadOnlyList<BuildMessage> Errors { get; }

    /// <summary>
    /// List of build warnings.
    /// </summary>
    IReadOnlyList<BuildMessage> Warnings { get; }

    /// <summary>
    /// List of build tests.
    /// </summary>
    IReadOnlyList<TestResult> Tests { get; }

    /// <summary>
    /// Summary of a build.
    /// </summary>
    BuildStatistics Summary { get; }
}