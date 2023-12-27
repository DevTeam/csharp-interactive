// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
namespace CSharpInteractive;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class ExitTracker(
    ISettings settings,
    IEnvironment environment,
    IPresenter<Summary> summaryPresenter,
    CancellationTokenSource cancellationTokenSource)
    : IExitTracker
{
    private volatile bool _isTerminating;

    public bool IsTerminating => _isTerminating;

    public IDisposable Track()
    {
        switch (settings.InteractionMode)
        {
            case InteractionMode.Interactive:
                System.Console.CancelKeyPress += ConsoleOnCancelKeyPress;
                return Disposable.Create(() => System.Console.CancelKeyPress -= ConsoleOnCancelKeyPress);

            default:
                AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
                return Disposable.Create(() => AppDomain.CurrentDomain.ProcessExit -= CurrentDomainOnProcessExit);
        }
    }

    private void CurrentDomainOnProcessExit(object? sender, EventArgs e)
    {
        _isTerminating = true;

        try
        {
            cancellationTokenSource.Cancel();
        }
        catch
        {
            // ignored
        }
        
        summaryPresenter.Show(Summary.Empty);
    }

    private void ConsoleOnCancelKeyPress(object? sender, ConsoleCancelEventArgs e) => environment.Exit(0);
}