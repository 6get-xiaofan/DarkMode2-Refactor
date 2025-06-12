namespace DarkMode.Core.Interfaces.Task;

/**
 * 任务
 */
public interface ITask
{
    string Name { get; }
    void Execute();
}