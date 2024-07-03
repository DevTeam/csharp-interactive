namespace CSharpInteractive.Core;

using HostApi;

internal interface IBuildMessageLogWriter
{
    void Write(BuildMessage message);
}