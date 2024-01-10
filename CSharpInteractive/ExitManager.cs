// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

[ExcludeFromCodeCoverage]
internal class ExitManager(
    ISettings settings,
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
        System.Environment.Exit(0);
    }
}