namespace HostApi.Internal.Cmd;

internal interface IVirtualContext
{
    bool IsActive { get; }
    
    string Resolve(string path);
}