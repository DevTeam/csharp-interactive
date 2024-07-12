namespace CSharpInteractive.Core;

using HostApi;

internal interface IBuildMessageLogWriter
{
    void Write(ProcessInfo processInfo, BuildMessage message);
}