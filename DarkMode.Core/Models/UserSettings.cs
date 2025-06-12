using DarkMode.Core.Enums;

namespace DarkMode.Core.Models;

public class UserSettings
{
    public ExecutionType ExecutionMode { get; set; } = ExecutionType.TimeSchedule;
    public LightConfig LightConfig { get; set; }
    public DarkConfig DarkConfig { get; set; }
    public string WallpaperEnginePath { get; set; } = null;
    public string Longitude { get; set; } = null;
    public string Latitude { get; set; } = null;
    public double AmbientLightSensorThreshold { get; set; } = 17.0;
    public int AmbientLightSensorStabilizationSeconds { get; set; }
    public float GpuUsage { get; set; }
    public int GpuUsageStabilizationSeconds { get; set; }
}

public class LightConfig
{
    public string Time { get; set; } = "6:00";
    public string WallpaperPath { get; set; } = null;
    public string LiveWallpaperPath { get; set; } = null;
    public SystemCursor SystemCursor { get; set; } = SystemCursor.WhiteCursor;
}

public class DarkConfig
{
    public string Time { get; set; } = "18:00";
    public string WallpaperPath { get; set; } = null;
    public string LiveWallpaperPath { get; set; } = null;
    public SystemCursor SystemCursor { get; set; } = SystemCursor.BlackCursor;
}
