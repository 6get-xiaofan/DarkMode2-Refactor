using System.IO;
using DarkMode.Core.Interfaces.Config;
using DarkMode.Core.Models;
using DarkMode.Core.Services.Config.ConfigFixer;
using DarkMode.Core.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DarkMode.Core.Services.Config;

public class ConfigService(ILogger<ConfigService> _logger) : IConfigService
{
    private readonly Dictionary<Type, object> _fixers = new()
    {
        { typeof(UserSettings), new UserSettingsFixer() },
        { typeof(AppSettings), new AppSettingsFixer() }
    };
    
    public void InitializeConfig<T>(string configPath, T defaultValue) where T : new()
        {
            try
            {
                if (!File.Exists(configPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(configPath)!);
                    File.WriteAllText(configPath, JsonConvert.SerializeObject(defaultValue, Formatting.Indented));
                    _logger.LogInformation($"Created new config: {Path.GetFileName(configPath)}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Init failed: {configPath} | {ex.Message}");
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
                _logger.LogError($"Load failed: {configPath} | {ex.Message}");
                return new T();
            }
        }

        public void SaveConfig<T>(string configPath, T config)
        {
            try
            {
                File.WriteAllText(configPath, JsonConvert.SerializeObject(config, Formatting.Indented));
                _logger.LogDebug("Saved config: {config}", Path.GetFileName(configPath));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Save failed: {configPath} | {ex.Message}");
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
                _logger.LogError($"Update failed: {configPath} | {ex.Message}");
            }
        }

        public void DeleteConfig(string configPath)
        {
            try
            {
                if (File.Exists(configPath))
                {
                    File.Delete(configPath);
                    _logger.LogInformation($"Deleted config: {Path.GetFileName(configPath)}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete failed: {configPath} | {ex.Message}");
            }
        }
        
        public void EnsureValidConfig<T>(string configPath, T defaultValue) where T : new()
        {
            try
            {
                T config;
                bool invalid = false;

                if (!File.Exists(configPath))
                {
                    config = defaultValue;
                    invalid = true;
                }
                else
                {
                    try
                    {
                        var json = File.ReadAllText(configPath);
                        config = JsonConvert.DeserializeObject<T>(json) ?? defaultValue;
                    }
                    catch
                    {
                        config = defaultValue;
                        invalid = true;
                    }
                }

                if (_fixers.TryGetValue(typeof(T), out var fixerObj) && fixerObj is IConfigFixer<T> fixer)
                {
                    fixer.MergeDefaults(config, defaultValue);
                }

                if (invalid)
                {
                    SaveConfig(configPath, config);
                    _logger.LogWarning("Reinitialized invalid config: {config}", Path.GetFileName(configPath));
                }
                else
                {
                    SaveConfig(configPath, config);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ensure config failed: {config} | {message}", configPath, ex.Message);
                throw;
            }
        }

}