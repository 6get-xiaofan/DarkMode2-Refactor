namespace DarkMode.Core.Interfaces.Config;

/**
 * 配置
 */
public interface IConfigService
{
    void InitializeConfig<T>(string configPath, T defaultValue) where T : new();
    T LoadConfig<T>(string configPath) where T : new();
    void SaveConfig<T>(string configPath, T config);
    void UpdateConfig<T>(string configPath, Action<T> updateAction) where T : new();
    void DeleteConfig(string configPath);
    void EnsureValidConfig<T>(string configPath, T defaultValue) where T : new();
}