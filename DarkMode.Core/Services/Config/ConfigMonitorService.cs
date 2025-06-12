using System.IO;
using DarkMode.Core.Interfaces.Config;
using Microsoft.Extensions.Logging;

namespace DarkMode.Core.Services.Config;

public class ConfigMonitorService<T> : IConfigMonitorService<T> where T : new()
{
    private FileSystemWatcher? _watcher;
    private string _configPath = string.Empty;

    private readonly ILogger<ConfigMonitorService<T>> _logger;
    private readonly IConfigService _configService;

    public event Action<T>? OnSettingsChanged;
    public T CurrentSettings { get; private set; } = new();

    public ConfigMonitorService(IConfigService configService)
    {
        _configService = configService;
        _logger = new LoggerFactory().CreateLogger<ConfigMonitorService<T>>();
    }

    public void StartMonitor(string configPath)
    {
        _configPath = configPath;
        CurrentSettings = _configService.LoadConfig<T>(configPath);

        _watcher = new FileSystemWatcher(Path.GetDirectoryName(configPath)!)
        {
            Filter = Path.GetFileName(configPath),
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
            EnableRaisingEvents = true
        };

        _watcher.Changed += (s, e) => Reload();
        _logger.LogInformation($"[ConfigMonitor<{typeof(T).Name}>] 开始监控：{_configPath}");
    }

    private void Reload()
    {
        try
        {
            Thread.Sleep(150); // 防止写入未完成

            var newSettings = _configService.LoadConfig<T>(_configPath);
            CurrentSettings = newSettings;

            _logger.LogInformation($"[ConfigMonitor<{typeof(T).Name}>] 配置已更新，触发回调");
            OnSettingsChanged?.Invoke(newSettings);
        }
        catch (Exception ex)
        {
            _logger.LogError($"[ConfigMonitor<{typeof(T).Name}>] 配置更新失败：{ex.Message}");
        }
    }
}