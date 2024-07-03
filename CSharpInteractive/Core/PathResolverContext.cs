// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using HostApi;
using HostApi.Cmd;

internal class PathResolverContext(IHost host) : IPathResolverContext, IVirtualContext
{
    private IPathResolver _currentResolver = EmptyResolver.Shared;
    private IPathResolver _prevResolver = EmptyResolver.Shared;

    public bool IsActive => _currentResolver != EmptyResolver.Shared;

    public IDisposable Register(IPathResolver resolver)
    {
        var prevResolver = _prevResolver;
        _prevResolver = _currentResolver;
        _currentResolver = resolver;
        return Disposable.Create(() =>
        {
            _currentResolver = _prevResolver;
            _prevResolver = prevResolver;
        });
    }

    public string Resolve(string path) => _currentResolver.Resolve(host, path, _prevResolver);

    private class EmptyResolver : IPathResolver
    {
        public static readonly IPathResolver Shared = new EmptyResolver();

        public string Resolve(IHost host, string path, IPathResolver nextResolver) => path;
    }
}