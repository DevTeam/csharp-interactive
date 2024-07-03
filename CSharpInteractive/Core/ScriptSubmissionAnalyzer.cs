// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using Microsoft.CodeAnalysis.CSharp;

[ExcludeFromCodeCoverage]
internal class ScriptSubmissionAnalyzer : IScriptSubmissionAnalyzer
{
    public bool IsCompleteSubmission(string script, CSharpParseOptions parseOptions) =>
        SyntaxFactory.IsCompleteSubmission(SyntaxFactory.ParseSyntaxTree(script, parseOptions));
}