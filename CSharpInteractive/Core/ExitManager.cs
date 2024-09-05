// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedType.Global
namespace CSharpInteractive.Core;

[ExcludeFromCodeCoverage]
internal class ExitManager(
    ISettings settings,
    IExitTracker exitTracker,
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    CancellationTokenSource cancellationTokenSource) : IActive
{
    public IDisposable Activate()
    {
        if (settings.InteractionMode != InteractionMode.Interactive)
        {
            return Disposable.Empty;
        }

        try
        {
            System.Console.TreatControlCAsInput = false;
            System.Console.CancelKeyPress += ConsoleOnCancelKeyPress;
        }
        catch
        {
            // ignored
        }

        return Disposable.Create(() => System.Console.CancelKeyPress -= ConsoleOnCancelKeyPress);
    }

    private void ConsoleOnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        cancellationTokenSource.Dispose();
        exitTracker.Exit(0);
    }
}