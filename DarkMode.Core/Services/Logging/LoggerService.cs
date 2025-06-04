using System.Globalization;
using System.IO;
using DarkMode.Core.Constants;
using DarkMode.Core.Enums;
using DarkMode.Core.Interfaces.Logging;

namespace DarkMode.Core.Services.Logging;

public class LoggerService : ILoggerService
{
    private LogLevel _currentLogLevel = LogLevel.Info;
    private readonly object _fileLock = new object();
    private DateTime _lastCleanupDate = DateTime.MinValue;
    
    public void SetLogLevel(LogLevel level) => _currentLogLevel = level;

    public void Debug(string module, string @namespace, string className, string method, string message) => 
        Log(LogLevel.Debug, module, @namespace, className, method, message);

    public void Info(string module, string @namespace, string className, string method, string message) => 
        Log(LogLevel.Info, module, @namespace, className, method, message);

    public void Warn(string module, string @namespace, string className, string method, string message) => 
        Log(LogLevel.Warn, module, @namespace, className, method, message);

    public void Error(string module, string @namespace, string className, string method, string message) => 
        Log(LogLevel.Error, module, @namespace, className, method, message);

    private void Log(LogLevel level, string module, string @namespace, string className, string method, string message)
    {
        if (level < _currentLogLevel) return;

        lock (_fileLock)
        {
            Directory.CreateDirectory(PathConstants.LogDirectory);

            if (DateTime.Now.Date > _lastCleanupDate)
            {
                CleanOldLogs();
                _lastCleanupDate = DateTime.Now.Date;
            }
            
            var logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                          $"{level} - " +
                          $"{module} - " +
                          $"{@namespace} - " +
                          $"{className}.cs - " +
                          $"{method}: " +
                          $"{message}";
            
            File.AppendAllText(GetLogFilePath(), logLine + Environment.NewLine);
        }
    }
    
    private string GetLogFilePath()
        => Path.Combine(PathConstants.LogDirectory, $"{DateTime.Now:yyyyMMdd}.log");

    private void CleanOldLogs()
    {
        foreach (var file in Directory.GetFiles(PathConstants.LogDirectory, "*.log"))
        {
            var fileDate = ExtractDateFromFileName(file);
            if (fileDate < DateTime.Now.AddDays(-7))
            {
                File.Delete(file);
            }
        }
    }
    
    private DateTime ExtractDateFromFileName(string filePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        return DateTime.ParseExact(fileName, "yyyyMMdd", CultureInfo.InvariantCulture);
    }
}