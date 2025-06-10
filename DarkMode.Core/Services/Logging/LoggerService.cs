using System.IO;
using DarkMode.Core.Constants;
using Serilog;

namespace DarkMode.Core.Services.Logging;

public class LoggerService
{
    private const string LogOutputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level}] ({SourceContext}): {Message:lj}{NewLine}{Exception}";
    public static ILogger CreateLogger()
    {
        return new LoggerConfiguration().WriteTo
            .Console(outputTemplate: LogOutputTemplate).WriteTo
            .File(
                Path.Combine(PathConstants.LogDirectory, $"DarkMode.log"), 
                rollingInterval: RollingInterval.Day, 
                outputTemplate: LogOutputTemplate,
                retainedFileCountLimit: 7
            )
            .CreateLogger();
    }
}