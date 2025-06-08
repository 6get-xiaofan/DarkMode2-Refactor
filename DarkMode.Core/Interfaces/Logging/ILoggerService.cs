using System.Runtime.CompilerServices;
using DarkMode.Core.Enums;

namespace DarkMode.Core.Interfaces.Logging;

/**
 * 日志
 */
public interface ILoggerService
{
    void SetLogLevel(LogLevel level);
    void Debug(string message, [CallerMemberName] string method = "");
    void Info(string message, [CallerMemberName] string method = "");
    void Warn(string message, [CallerMemberName] string method = "");
    void Error(string message, [CallerMemberName] string method = "");
    
}