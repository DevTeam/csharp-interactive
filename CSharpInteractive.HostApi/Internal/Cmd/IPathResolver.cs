// ReSharper disable UnusedParameter.Global
namespace HostApi.Internal.Cmd;

internal interface IPathResolver
{
    string Resolve(
        IHost host,
        string path,
        IPathResolver nextResolver);
}