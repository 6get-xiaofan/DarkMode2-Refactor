using DarkMode.Core.Models;

namespace DarkMode.Core.Utils;

public class UserSettingsFixer
{
    public static void MergeDefaults(UserSettings current, UserSettings defaults)
    {
        if (current == null || defaults == null) return;

        current.ExecutionMode = current.ExecutionMode;

        // 合并 LightConfig
        if (current.LightConfig == null)
            current.LightConfig = defaults.LightConfig;
        else
            MergeLightConfig(current.LightConfig, defaults.LightConfig);

        // 合并 DarkConfig
        if (current.DarkConfig == null)
            current.DarkConfig = defaults.DarkConfig;
        else
            MergeDarkConfig(current.DarkConfig, defaults.DarkConfig);

        current.WallpaperEnginePath ??= defaults.WallpaperEnginePath;
        current.Latitude ??= defaults.Latitude;
        current.Longitude ??= defaults.Longitude;

        if (current.AmbientLightSensorThreshold <= 0)
            current.AmbientLightSensorThreshold = defaults.AmbientLightSensorThreshold;

        if (current.AmbientLightSensorStabilizationSeconds <= 0)
            current.AmbientLightSensorStabilizationSeconds = defaults.AmbientLightSensorStabilizationSeconds;

        if (current.GpuUsage <= 0)
            current.GpuUsage = defaults.GpuUsage;

        if (current.GpuUsageStabilizationSeconds <= 0)
            current.GpuUsageStabilizationSeconds = defaults.GpuUsageStabilizationSeconds;
    }

    private static void MergeLightConfig(LightConfig current, LightConfig defaults)
    {
        current.Time ??= defaults.Time;
        current.WallpaperPath ??= defaults.WallpaperPath;
        current.LiveWallpaperPath ??= defaults.LiveWallpaperPath;
        current.SystemCursor = current.SystemCursor;
    }

    private static void MergeDarkConfig(DarkConfig current, DarkConfig defaults)
    {
        current.Time ??= defaults.Time;
        current.WallpaperPath ??= defaults.WallpaperPath;
        current.LiveWallpaperPath ??= defaults.LiveWallpaperPath;
        current.SystemCursor = current.SystemCursor;
    }
}