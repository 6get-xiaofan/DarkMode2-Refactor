using DarkMode.Core.Interfaces.Logging;

namespace DarkMode.Core.Services.Logging;

public class LoggerServiceFactory : ILoggerServiceFactory
{
    public ILoggerService CreateLogger<T>(string module)
    {
        return new LoggerService(module, typeof(T));
    }
}