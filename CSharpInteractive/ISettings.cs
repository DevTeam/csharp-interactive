// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedMember.Global
namespace CSharpInteractive;

internal interface ISettings
{
    IReadOnlyList<string> ScriptArguments { get; }

    IReadOnlyDictionary<string, string> ScriptProperties { get; }

    VerbosityLevel VerbosityLevel { get; }

    InteractionMode InteractionMode { get; }

    bool ShowHelpAndExit { get; }

    bool ShowVersionAndExit { get; }

    IEnumerable<ICodeSource> CodeSources { get; }

    IEnumerable<string> NuGetSources { get; }
}