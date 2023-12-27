namespace CSharpInteractive;

internal class Setting<T>(T defaultSettingValue) : ISettingGetter<T>, ISettingSetter<T>
    where T: struct, Enum
{

    public T GetSetting() => defaultSettingValue;

    public T SetSetting(T value)
    {
        var prevValue = defaultSettingValue;
        defaultSettingValue = value;
        return prevValue;
    }
}