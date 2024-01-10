// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

[ExcludeFromCodeCoverage]
internal class VerbosityLevelSettingDescription : ISettingDescription
{
    public bool IsVisible => true;

    public Type SettingType => typeof(VerbosityLevel);

    public string Key => "l";

    public string Description => "Set a verbosity level";
}