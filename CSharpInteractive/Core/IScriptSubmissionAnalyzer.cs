namespace CSharpInteractive.Core;

using Microsoft.CodeAnalysis.CSharp;

internal interface IScriptSubmissionAnalyzer
{
    bool IsCompleteSubmission(string script, CSharpParseOptions parseOptions);
}