using DarkMode.Core.Enums;

namespace DarkMode.Core.Interfaces.Logging;

public interface ILoggerService
{
    void SetLogLevel(LogLevel level);
    void Debug(string module, string @namespace, string className, string method, string message);
    void Info(string module, string @namespace, string className, string method, string message);
    void Warn(string module, string @namespace, string className, string method, string message);
    void Error(string module, string @namespace, string className, string method, string message);
}