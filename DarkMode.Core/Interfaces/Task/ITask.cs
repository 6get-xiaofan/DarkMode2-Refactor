using DarkMode.Core.Enums;

namespace DarkMode.Core.Interfaces.Task;

/**
 * 任务
 */
public interface ITask
{
    System.Threading.Tasks.Task ExecuteAsync(SystemTheme theme);
}