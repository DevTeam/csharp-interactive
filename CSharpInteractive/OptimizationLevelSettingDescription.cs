// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using Microsoft.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class OptimizationLevelSettingDescription : ISettingDescription
{
    public bool IsVisible => false;

    public Type SettingType => typeof(OptimizationLevel);

    public string Key => "ol";

    public string Description => "Set an optimization level";
}