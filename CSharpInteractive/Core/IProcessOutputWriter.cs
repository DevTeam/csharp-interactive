namespace CSharpInteractive.Core;

using HostApi;

internal interface IProcessOutputWriter
{
    void Write(in Output output);
}