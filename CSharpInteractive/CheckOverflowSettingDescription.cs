// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

[ExcludeFromCodeCoverage]
internal class CheckOverflowSettingDescription : ISettingDescription
{
    public bool IsVisible => false;

    public Type SettingType => typeof(CheckOverflow);

    public string Key => "co";

    public string Description => "Enable or disable overflow check";
}