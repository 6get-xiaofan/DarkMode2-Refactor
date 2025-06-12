namespace DarkMode.Core.Interfaces.Config;

public interface IConfigMonitorService<T> where T : new()
{
    event Action<T> OnSettingsChanged;

    T CurrentSettings { get; }
    void StartMonitor(string configPath);
}