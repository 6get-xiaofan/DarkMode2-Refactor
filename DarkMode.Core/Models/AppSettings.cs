using DarkMode.Core.Enums;

namespace DarkMode.Core.Models;

public class AppSettings
{
    public bool AutoStart
    {
        get;
        set;
    } = false;

    public bool Notifications
    {
        get;
        set;
    } = true;

    public bool TrayIcon
    {
        get;
        set;
    } = true;

    public string UpdateChannel
    {
        get;
        set;
    } = "stable";

    public bool AutoUpdate
    {
        get;
        set;
    } = false;

    public string Theme
    {
        get;
        set;
    } = "system";

    public string Language
    {
        get;
        set;
    } = "zh-CN";
    
    public bool DeveloperMode
    {
        get;
        set;
    } = false;

    public LogLevel LogLevel
    {
        get;
        set;
    } =
#if DEBUG
        LogLevel.Debug;
#else
        LogLevel.Info;
#endif
}