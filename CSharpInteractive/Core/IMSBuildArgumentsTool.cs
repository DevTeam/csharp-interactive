// ReSharper disable InconsistentNaming

namespace CSharpInteractive.Core;

internal interface IMSBuildArgumentsTool
{
    string Unescape(string escaped);
}