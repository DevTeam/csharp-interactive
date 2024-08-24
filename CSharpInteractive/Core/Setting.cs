namespace CSharpInteractive.Core;

internal class Setting<T>(T defaultSettingValue) : ISettingGetter<T>, ISettingSetter<T>
    where T: struct, Enum
{
    private T _defaultSettingValue = defaultSettingValue;

    public T GetSetting() => _defaultSettingValue;

    public T SetSetting(T value)
    {
        var prevValue = _defaultSettingValue;
        _defaultSettingValue = value;
        return prevValue;
    }
}