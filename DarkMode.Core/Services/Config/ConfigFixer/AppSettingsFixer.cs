using DarkMode.Core.Models;
using Serilog.Events;

namespace DarkMode.Core.Services.Config.ConfigFixer;

public class AppSettingsFixer : SettingsFixer<AppSettings>
{
    public override void MergeDefaults(AppSettings current, AppSettings defaults)
    {
        if (current == null || defaults == null) return;

        current.IsInitialized = current.IsInitialized || defaults.IsInitialized;
        current.AutoStart = current.AutoStart || defaults.AutoStart;
        current.Notifications = current.Notifications || defaults.Notifications;
        current.TrayIcon = current.TrayIcon || defaults.TrayIcon;
        current.AutoUpdate = current.AutoUpdate || defaults.AutoUpdate;
        current.DeveloperMode = current.DeveloperMode || defaults.DeveloperMode;

        current.UpdateChannel ??= defaults.UpdateChannel;
        current.Theme ??= defaults.Theme;
        current.Language ??= defaults.Language;

        if (!Enum.IsDefined(typeof(LogEventLevel), current.LogLevel))
            current.LogLevel = defaults.LogLevel;
    }
}