using DarkMode.Core.Interfaces.Config;

namespace DarkMode.Core.Services.Config.ConfigFixer;

public class SettingsFixer<T> : IConfigFixer<T> where T : class, new()
{
    public virtual void MergeDefaults(T current, T defaults)
    {
    }
}