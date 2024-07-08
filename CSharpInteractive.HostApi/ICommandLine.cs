// ReSharper disable UnusedParameter.Global
namespace HostApi;

using System.Diagnostics.Contracts;

public interface ICommandLine
{
    [Pure]
    IStartInfo GetStartInfo(IHost host);
}