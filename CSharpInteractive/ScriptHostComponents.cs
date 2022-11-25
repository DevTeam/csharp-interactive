// ReSharper disable NotAccessedPositionalProperty.Global
namespace CSharpInteractive;

using System.Diagnostics.CodeAnalysis;
using HostApi;

[ExcludeFromCodeCoverage]
internal readonly record struct ScriptHostComponents(
    IHost Host,
    IStatistics Statistics,
    ILog<ScriptHostComponents> Log,
    IInfo Info,
    IExitTracker ExitTracker,
    ICommandLineRunner CommandLineRunner,
    IBuildRunner BuildRunner);