// ReSharper disable UnusedParameter.Global
namespace HostApi.Internal.Cmd;

using System.Diagnostics.Contracts;

internal interface IPathResolver
{
    [Pure]
    string Resolve(
        IHost host,
        string path,
        IPathResolver nextResolver);
}