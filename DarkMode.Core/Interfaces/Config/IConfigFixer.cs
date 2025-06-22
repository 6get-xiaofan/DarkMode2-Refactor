namespace DarkMode.Core.Interfaces.Config;

public interface IConfigFixer<T>
{
    void MergeDefaults(T current, T defaults);
}