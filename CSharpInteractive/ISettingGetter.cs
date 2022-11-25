namespace CSharpInteractive;

internal interface ISettingGetter<out TSetting>
    where TSetting: struct, Enum
{
    TSetting GetSetting();
}