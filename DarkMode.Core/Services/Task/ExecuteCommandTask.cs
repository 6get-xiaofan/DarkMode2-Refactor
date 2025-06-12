using DarkMode.Core.Interfaces.Task;

namespace DarkMode.Core.Services.Task;

public class ExecuteCommandTask : TaskBase
{
    public override string Name => "ExecuteCommand";
    protected override void ExecuteTask()
    {
        // TODO: 实现执行系统命令逻辑
        Console.WriteLine("正在执行命令...");
    }
}