using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using DarkMode.Core.Constants;
using DarkMode.Core.Enums;
using DarkMode.Core.Interfaces.Logging;

namespace DarkMode.Core.Services.Logging;

public class LoggerService : ILoggerService
{
    private readonly string _module;
    private readonly Type _sourceType;
    private LogLevel _currentLogLevel = LogLevel.Info;
    private static readonly object FileLock = new object();
    private DateTime _lastCleanupDate = DateTime.MinValue;
    
    public LoggerService(string module, Type sourceType)
    {
        _module = module;
        _sourceType = sourceType;
    }
    
    public void SetLogLevel(LogLevel level) => _currentLogLevel = level;
    
    public void Debug(string message, [CallerMemberName] string method = "") => 
        Log(LogLevel.Debug, message, method);

    public void Info(string message, [CallerMemberName] string method = "") =>
        Log(LogLevel.Info, message, method);
    
    public void Warn(string message, [CallerMemberName] string method = "") =>
        Log(LogLevel.Warn, message, method);
    
    public void Error(string message, [CallerMemberName] string method = "") =>
        Log(LogLevel.Error, message, method);

    private void Log(LogLevel level, string message, string method)
    {
        if (level < _currentLogLevel) return;

        // 自动获取命名空间和类名
        string namespaceName = _sourceType.Namespace;
        string className = _sourceType.Name;

        lock (FileLock)
        {
            Directory.CreateDirectory(PathConstants.LogDirectory);
            if (DateTime.Now.Date > _lastCleanupDate)
            {
                CleanOldLogs();
                _lastCleanupDate = DateTime.Now.Date;
            }

            var logLine = 
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                $"{level} - " +
                $"{_module} - " +
                $"{namespaceName} - " +
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