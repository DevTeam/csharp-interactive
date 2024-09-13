// ReSharper disable UnusedParameter.Global

namespace HostApi;

using System.Diagnostics.Contracts;

/// <summary>
/// Represents an abstraction of the command line.
/// <example>
/// <code>
/// var cmd = new CommandLine("whoami");
/// cmd.Run().EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <seealso cref="CommandLine"/>
/// <seealso cref="ICommandLineRunner"/>
/// <seealso cref="IBuildRunner"/>
public interface ICommandLine
{
    /// <summary>
    /// Determines process startup information.
    /// </summary>
    /// <param name="host">Process host.</param>
    /// <returns>Process startup information.</returns>
    [Pure]
    IStartInfo GetStartInfo(IHost host);
}