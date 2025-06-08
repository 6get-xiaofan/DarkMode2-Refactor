namespace DarkMode.Core.Interfaces.Logging;

public interface ILoggerServiceFactory
{
    ILoggerService CreateLogger<T>(string module);
}