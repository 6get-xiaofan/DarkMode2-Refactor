using System.IO;

namespace DarkMode.Core.Constants;

public static class PathConstants
{
    public static string LogDirectory =>  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
}