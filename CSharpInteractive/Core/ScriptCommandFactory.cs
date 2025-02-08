// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using System.Text;
using HostApi;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

internal class ScriptCommandFactory(
    ILog<ScriptCommandFactory> log,
    IScriptSubmissionAnalyzer scriptSubmissionAnalyzer) : ICommandFactory<ScriptCommand>
{
    internal static readonly CSharpParseOptions ParseOptions = new(LanguageVersion.Latest, kind: SourceCodeKind.Script);
    private readonly StringBuilder _scriptBuilder = new();

    public int Order => 0;

    public IEnumerable<ICommand> Create(ScriptCommand scriptCommand)
    {
        _scriptBuilder.AppendLine(scriptCommand.Script);
        var script = _scriptBuilder.ToString();
        if (scriptSubmissionAnalyzer.IsCompleteSubmission(script, ParseOptions))
        {
            log.Trace(() => [new Text("Completed submission")]);
            _scriptBuilder.Clear();
            yield return new ScriptCommand(scriptCommand.Name, script, scriptCommand.Internal);
            yield break;
        }

        log.Trace(() => [new Text("Incomplete submission")]);
        yield return new CodeCommand(scriptCommand.Internal);
    }
}