// ReSharper disable NotAccessedPositionalProperty.Global
namespace CSharpInteractive.Core;

using HostApi;

internal record ProcessResult(
    IStartInfo StartInfo,
    ProcessState State,
    long ElapsedMilliseconds,
    Text[] Description,
    int? ExitCode = default,
    Exception? Error = default);