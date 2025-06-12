using DarkMode.Core.Enums;
using DarkMode.Core.Interfaces.Execution;

namespace DarkMode.Core.Services.Execution;

public class TimeScheduleModeService : IExecutionMode
{
    public ExecutionType Name => ExecutionType.TimeSchedule;

    public bool ShouldExecute()
    {
        // TODO: 读取当前时间与用户配置对比
        return DateTime.Now.Hour >= 20 || DateTime.Now.Hour < 6;
    }
}