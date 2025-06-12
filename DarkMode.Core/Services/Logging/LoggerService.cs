using System.IO;
using DarkMode.Core.Constants;
using DarkMode.Core.Interfaces.Config;
using DarkMode.Core.Models;
using DarkMode.Core.Services.Config;
using DarkMode.Core.Utils;
using Serilog;
using Serilog.Events;

namespace DarkMode.Core.Services.Logging;

public class LoggerService
{
    private readonly IConfigService _configService = new ConfigService(null);
    private const string LogOutputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level}] ({SourceContext}): {Message:lj}{NewLine}{Exception}";
    public static ILogger CreateLogger()
    {
        var config = new LoggerConfiguration().WriteTo
            .Console(outputTemplate: LogOutputTemplate).WriteTo
            .File(
                Path.Combine(PathConstants.LogDirectory, $"DarkMode.log"),
                rollingInterval: RollingInterval.Day,
                outputTemplate: LogOutputTemplate,
                retainedFileCountLimit: 7
            );
        
        config = EnvHelper.IsDebugMode ? config.MinimumLevel.Debug() : config.MinimumLevel.Is(new LoggerService().GetLogLevel());

        return config.CreateLogger();
    }

    private LogEventLevel GetLogLevel()
    {
        var level = _configService.LoadConfig<AppSettings>(PathConstants.AppSettingsPath);
        return level.LogLevel;
    }
}