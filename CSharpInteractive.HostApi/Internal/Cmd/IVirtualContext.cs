namespace HostApi.Internal.Cmd;

using System.Diagnostics.Contracts;

internal interface IVirtualContext
{
    bool IsActive { get; }
    
    [Pure]
    string Resolve(string path);
}