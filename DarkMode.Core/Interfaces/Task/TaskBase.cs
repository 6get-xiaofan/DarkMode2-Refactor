using DarkMode.Core.Interfaces.Task;

namespace DarkMode.Core.Interfaces.Task;

public abstract class TaskBase: ITask
{
    public abstract string Name { get; }

    // 模板方法：统一执行流程（可加入前后处理逻辑）
    public void Execute()
    {
        // 可加入日志、异常处理等公共逻辑
        Console.WriteLine($"[Task Start] {Name}");
        ExecuteTask();
        Console.WriteLine($"[Task End] {Name}");
    }

    // 子类实现具体执行逻辑
    protected abstract void ExecuteTask();
}