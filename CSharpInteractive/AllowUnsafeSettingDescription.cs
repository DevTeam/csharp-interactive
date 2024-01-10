// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

[ExcludeFromCodeCoverage]
internal class AllowUnsafeSettingDescription : ISettingDescription
{
    public bool IsVisible => false;

    public Type SettingType => typeof(AllowUnsafe);

    public string Key => "au";

    public string Description => "Allow or do not allow unsafe code";
}