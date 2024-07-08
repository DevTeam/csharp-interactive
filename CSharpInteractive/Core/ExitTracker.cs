// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
namespace CSharpInteractive.Core;

using System.Reflection;

[ExcludeFromCodeCoverage]
internal class ExitTracker(
    ISettings settings,
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
    
    public void Exit(int exitCode)
    {
        Finish();
        ClearEvents(typeof(AppContext));
        System.Environment.Exit(exitCode);
    }
    
    private static void ClearEvents(Type type)
    {
        var events = type.GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        foreach (var eventInfo in events)
        {
            var fieldInfo = type.GetField(eventInfo.Name, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            if (fieldInfo?.GetValue(default) is not Delegate eventHandler)
            {
                continue;
            }

            foreach (var invocation in eventHandler.GetInvocationList())
            {
                eventInfo?.GetRemoveMethod(fieldInfo.IsPrivate)?.Invoke(default, [invocation]);
            }
        }
    }
    
    private void CurrentDomainOnProcessExit(object? sender, EventArgs e) =>
        Finish();

    private void ConsoleOnCancelKeyPress(object? sender, ConsoleCancelEventArgs e) => 
        Exit(0);

    private void Finish()
    {
        if (_isTerminating)
        {
            return;
        }
        
        _isTerminating = true;
        summaryPresenter.Show(Summary.Empty);
        try
        {
            cancellationTokenSource.Cancel();
        }
        catch (Exception)
        {
            //
        }
    }
}