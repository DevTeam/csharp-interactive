// ReSharper disable InconsistentNaming
namespace HostApi.Internal.DotNet;

internal interface IDotNetSettings
{
    bool LoggersAreRequired { get; }

    string DotNetExecutablePath { get; }

    string DotNetMSBuildLoggerDirectory { get; }

    string DotNetVSTestLoggerDirectory { get; }

    string TeamCityMessagesPath { get; }
}