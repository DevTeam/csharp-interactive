namespace HostApi;

/// <summary>
/// Provides process startup information.
/// </summary>
public interface IStartInfo
{
    /// <summary>
    /// Short name of the command line.
    /// </summary>
    string ShortName { get; }

    /// <summary>
    /// The path to the file to be executed.
    /// </summary>
    string ExecutablePath { get; }

    /// <summary>
    /// Working directory.
    /// </summary>
    string WorkingDirectory { get; }

    /// <summary>
    /// Command line arguments passed to the process.
    /// </summary>
    IEnumerable<string> Args { get; }

    /// <summary>
    /// Environment variables passed to the process.
    /// </summary>
    IEnumerable<(string name, string value)> Vars { get; }
}