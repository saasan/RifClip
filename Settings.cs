using System.Configuration;

public sealed class Settings : ApplicationSettingsBase
{
    // Singleton instance of Settings
    private static readonly Settings _instance = new Settings();
    public static Settings Instance => _instance;

    private Settings() { }

    // 画像を削除時に通知する
    [UserScopedSetting]
    [DefaultSettingValue("true")]
    public bool NotifyOnRemoveImage
    {
        get => (bool)this[nameof(NotifyOnRemoveImage)];
        set => this[nameof(NotifyOnRemoveImage)] = value;
    }
}
