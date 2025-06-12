using DarkMode.Core.Constants;
using DarkMode.Core.Enums;
using DarkMode.Core.Interfaces.Execution;
using DarkMode.Core.Models;
using DarkMode.Core.Services.Config;

namespace DarkMode.Core.Services.Execution.Factory;

public class ExecutionModeFactory : IExecutionModeFactory
{
    private readonly ConfigService _configService;
    private readonly TimeScheduleModeService _timeScheduleMode;
    private readonly SensorModeService _sensorMode;

    public ExecutionModeFactory(
        ConfigService configService,
        TimeScheduleModeService timeScheduleMode,
        SensorModeService sensorMode)
    {
        _configService = configService;
        _timeScheduleMode = timeScheduleMode;
        _sensorMode = sensorMode;
    }

    public IExecutionMode GetExecutionMode()
    {
        var config = _configService.LoadConfig<UserSettings>(PathConstants.UserSettingsPath);
        return config.ExecutionMode switch
        {
            ExecutionType.TimeSchedule => _timeScheduleMode,
            ExecutionType.Sensor => _sensorMode,
            _ => _timeScheduleMode
        };
    }
    
    public IExecutionMode GetExecutionMode(ExecutionType type)
    {
        return type switch
        {
            ExecutionType.TimeSchedule => _timeScheduleMode,
            ExecutionType.Sensor => _sensorMode,
            _ => _timeScheduleMode
        };
    }
}