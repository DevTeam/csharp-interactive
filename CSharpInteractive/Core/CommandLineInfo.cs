// ReSharper disable NotAccessedPositionalProperty.Global
namespace CSharpInteractive.Core;

using HostApi;

internal record CommandLineInfo(
    ICommandLineResult CommandLineResult,
    ProcessResult ProcessResult);