// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
namespace CSharpInteractive.Core;

using Microsoft.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class DiagnosticsPresenter(ILog<DiagnosticsPresenter> log, IErrorContext errorContext) : IPresenter<CompilationDiagnostics>
{
    public void Show(CompilationDiagnostics data)
    {
        var (sourceCommand, readOnlyCollection) = data;
        var prefix = Text.Empty;
        if(errorContext.TryGetSourceName(out var name))
        {
            prefix = new Text(name + " ");
        }

        foreach (var diagnostic in readOnlyCollection)
        {
            switch (diagnostic.Severity)
            {
                case DiagnosticSeverity.Hidden:
                    log.Trace(() => [prefix, new Text(diagnostic.ToString())]);
                    break;

                case DiagnosticSeverity.Info:
                    log.Info(prefix, new Text(diagnostic.ToString()));
                    break;

                case DiagnosticSeverity.Warning:
                    log.Warning(prefix, new Text(diagnostic.ToString()));
                    break;

                case DiagnosticSeverity.Error:
                    var errorId = $"{GetProperty(diagnostic.Id, string.Empty)},{diagnostic.Location.SourceSpan.Start},{diagnostic.Location.SourceSpan.Length}{GetProperty(GetFileName(sourceCommand.Name))}";
                    log.Error(new ErrorId(errorId), prefix, new Text(diagnostic.ToString()));
                    break;
            }
        }
    }

    private static string GetProperty(string? value, string prefix = ",") =>
        string.IsNullOrWhiteSpace(value) ? string.Empty : $"{prefix}{value}";

    private static string GetFileName(string file)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(file))
            {
                return Path.GetFileName(file);
            }
        }
        catch
        {
            // ignored
        }

        return string.Empty;
    }
}