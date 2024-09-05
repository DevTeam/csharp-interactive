// ReSharper disable UnusedMemberInSuper.Global
namespace CSharpInteractive.Core;

internal interface ITestEnvironment
{
    public bool IsTesting { get; set; }

    public int? ExitCode { get; set; }
}