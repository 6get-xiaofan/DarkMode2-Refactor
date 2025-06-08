using System.IO;

namespace DarkMode.Core.Constants;

public static class PathConstants
{
    public static string LogDirectory =>  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
    public static string AppSettingsPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appSettings.json");
        
    public static string UserSettingsPath => 
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DarkMode\\userSettings.json");
}