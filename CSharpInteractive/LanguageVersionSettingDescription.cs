// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

[ExcludeFromCodeCoverage]
internal class LanguageVersionSettingDescription : ISettingDescription
{
    public bool IsVisible => false;

    public Type SettingType => typeof(LanguageVersion);

    public string Key => "lv";

    public string Description => "Set a C# language version";
}