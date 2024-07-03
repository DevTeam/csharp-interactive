namespace CSharpInteractive.Core;

using System.Diagnostics;
using HostApi;

internal interface IStartInfoFactory
{
    ProcessStartInfo Create(IStartInfo info);
}