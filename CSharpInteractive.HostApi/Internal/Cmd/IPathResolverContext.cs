namespace HostApi.Internal.Cmd;

using System.Diagnostics.Contracts;

internal interface IPathResolverContext
{
    [Pure]
    IDisposable Register(IPathResolver resolver);
}