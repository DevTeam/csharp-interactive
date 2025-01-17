// ReSharper disable NotAccessedPositionalProperty.Global

namespace CSharpInteractive.Core;

using HostApi;

internal record ProcessResult(
    ProcessInfo ProcessInfo,
    ProcessState State,
    long ElapsedMilliseconds,
    Text[] Description,
    int? ExitCode = null,
    Exception? Error = null);