namespace HostApi.Internal.Cmd;

internal interface IPathResolverContext
{
    IDisposable Register(IPathResolver resolver);
}