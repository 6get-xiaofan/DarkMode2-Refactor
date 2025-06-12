using DarkMode.Core.Enums;
using DarkMode.Core.Interfaces.Execution;

namespace DarkMode.Core.Services.Execution;

public class SensorModeService : IExecutionMode
{
    public ExecutionType Name => ExecutionType.Sensor;

    public bool ShouldExecute()
    {
        // TODO: 读取传感器数据，光照值是否满足
        return false;
    }
}