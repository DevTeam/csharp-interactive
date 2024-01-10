// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

[ExcludeFromCodeCoverage]
internal class CSharpScriptRunner(
    ILog<CSharpScriptRunner> log,
    IPresenter<ScriptState<object>> scriptStatePresenter,
    IPresenter<CompilationDiagnostics> diagnosticsPresenter,
    // ReSharper disable once ParameterTypeCanBeEnumerable.Local
    IReadOnlyCollection<IScriptOptionsFactory> scriptOptionsFactories,
    IExitCodeParser exitCodeParser)
    : ICSharpScriptRunner
{
    private ScriptState<object>? _scriptState;

    public CommandResult Run(ICommand sourceCommand, string script)
    {
        var success = true;
        try
        {
            var options = scriptOptionsFactories.Aggregate(ScriptOptions.Default, (current, scriptOptionsFactory) => scriptOptionsFactory.Create(current));
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _scriptState =
                (_scriptState ?? CSharpScript.RunAsync(string.Empty, options).Result)
                .ContinueWithAsync(
                    script,
                    options,
                    exception =>
                    {
                        success = false;
                        log.Trace(() => [new Text($"Exception: {exception}.")]);
                        log.Error(ErrorId.Exception, [new Text(exception.ToString())]);
                        return true;
                    })
                .Result;

            stopwatch.Stop();
            log.Trace(() => [new Text($"Time Elapsed {stopwatch.Elapsed:g}")]);
            diagnosticsPresenter.Show(new CompilationDiagnostics(sourceCommand, _scriptState.Script.GetCompilation().GetDiagnostics().ToList().AsReadOnly()));
            if (_scriptState.ReturnValue != default)
            {
                if (success && exitCodeParser.TryParse(_scriptState.ReturnValue, out var exitCode))
                {
                    return new CommandResult(sourceCommand, success, exitCode);
                }

                log.Trace(() => [new Text("The return value is \""), new Text(_scriptState.ReturnValue.ToString() ?? "empty"), new Text("\".")]);
            }
            else
            {
                log.Trace(() => [new Text("The return value is \"null\".")]);
            }
        }
        catch (CompilationErrorException e)
        {
            diagnosticsPresenter.Show(new CompilationDiagnostics(sourceCommand, e.Diagnostics.ToList().AsReadOnly()));
            success = false;
        }
        finally
        {
            if (_scriptState != null)
            {
                scriptStatePresenter.Show(_scriptState);
            }
        }

        return new CommandResult(sourceCommand, success);
    }

    public void Reset()
    {
        log.Trace(() => [new Text("Reset state.")]);
        _scriptState = default;
    }
}