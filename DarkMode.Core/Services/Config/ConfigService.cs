using System.IO;
using DarkMode.Core.Interfaces.Config;
using DarkMode.Core.Services.Logging;
using Newtonsoft.Json;
using Serilog;

namespace DarkMode.Core.Services.Config;

public class ConfigService : IConfigService
{
    private readonly ILogger _logger = LoggerService.CreateLogger();

    public void InitializeConfig<T>(string configPath, T defaultValue) where T : new()
        {
            try
            {
                if (!File.Exists(configPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(configPath)!);
                    File.WriteAllText(configPath, JsonConvert.SerializeObject(defaultValue, Formatting.Indented));
                    _logger.Information($"Created new config: {Path.GetFileName(configPath)}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Init failed: {configPath} | {ex.Message}");
                throw;
            }
        }

        public T LoadConfig<T>(string configPath) where T : new()
        {
            try
            {
                var json = File.ReadAllText(configPath);
                return JsonConvert.DeserializeObject<T>(json) ?? new T();
            }
            catch (Exception ex)
            {
                _logger.Error($"Load failed: {configPath} | {ex.Message}");
                return new T();
            }
        }

        public void SaveConfig<T>(string configPath, T config)
        {
            try
            {
                File.WriteAllText(configPath, JsonConvert.SerializeObject(config, Formatting.Indented));
                _logger.Debug($"Saved config: {Path.GetFileName(configPath)}");
            }
            catch (Exception ex)
            {
                _logger.Error($"Save failed: {configPath} | {ex.Message}");
            }
        }

        public void UpdateConfig<T>(string configPath, Action<T> updateAction) where T : new()
        {
            try
            {
                var config = LoadConfig<T>(configPath);
                updateAction(config);
                SaveConfig(configPath, config);
            }
            catch (Exception ex)
            {
                _logger.Error($"Update failed: {configPath} | {ex.Message}");
            }
        }

        public void DeleteConfig(string configPath)
        {
            try
            {
                if (File.Exists(configPath))
                {
                    File.Delete(configPath);
                    _logger.Information($"Deleted config: {Path.GetFileName(configPath)}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Delete failed: {configPath} | {ex.Message}");
            }
        }
}