namespace CSharpInteractive;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using HostApi;

internal static class CSharpInteractiveHostInitializer
{
    [ModuleInitializer]
    [SuppressMessage("Usage", "CA2255:The \'ModuleInitializer\' attribute should not be used in libraries")]
    [SuppressMessage("Performance", "CA1806:Do not ignore method results")]
    public static void Initialize()
    {
        // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
        Host.GetService<IHost>();
    }
}