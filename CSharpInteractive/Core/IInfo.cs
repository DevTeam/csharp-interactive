// ReSharper disable UnusedMember.Global

namespace CSharpInteractive.Core;

internal interface IInfo
{
    void ShowHeader();

    void ShowReplHelp();

    void ShowHelp();

    void ShowVersion();

    void ShowFooter();
}