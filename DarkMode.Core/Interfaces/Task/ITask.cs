using DarkMode.Core.Enums;

namespace DarkMode.Core.Interfaces.Task;

public interface ITask
{
    System.Threading.Tasks.Task ExecuteAsync(SystemTheme theme);
}