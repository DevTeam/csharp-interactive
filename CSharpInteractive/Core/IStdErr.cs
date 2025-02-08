// ReSharper disable UnusedMember.Global

namespace CSharpInteractive.Core;

using HostApi;

internal interface IStdErr
{
    void WriteLine(params Text[] errorLine);
}