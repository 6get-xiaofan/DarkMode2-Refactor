using System.Timers;
using DarkMode.Core.Constants;
using DarkMode.Core.Domains;
using DarkMode.Core.Interfaces.Config;
using DarkMode.Core.Interfaces.Execution;
using DarkMode.Core.Interfaces.Scheduler;
using DarkMode.Core.Models;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace DarkMode.Core.Services.Scheduler;

public class SchedulerService: ISchedulerService
{
    private readonly IExecutionModeFactory _modeFactory;
    private readonly IEnumerable<ScheduledJob> _jobs;
    private readonly ILogger<SchedulerService> _logger;
    private IExecutionMode _currentMode;
    private readonly IConfigMonitorService<UserSettings> _userSettingsMonitor;
    private readonly Timer _timer;

    public SchedulerService(IExecutionModeFactory modeFactory, IEnumerable<ScheduledJob> jobs, IConfigMonitorService<UserSettings> userSettingsMonitor)
    {
        _modeFactory = modeFactory;
        _currentMode = _modeFactory.GetExecutionMode();
        _jobs = jobs;
        _userSettingsMonitor = userSettingsMonitor;
        _userSettingsMonitor.OnSettingsChanged += OnUserSettingsChanged;
        _logger = new LoggerFactory().CreateLogger<SchedulerService>();
        _timer = new Timer(1000);
        _timer.Elapsed += OnTimerElapsed;
    }

    public void Start()
    {
        _userSettingsMonitor.StartMonitor(PathConstants.UserSettingsPath);
        BuildAndStartMode(_userSettingsMonitor.CurrentSettings);
    }
    public void Stop() => _timer.Stop();

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        if (_currentMode == null) return;

        if (_currentMode.ShouldExecute())
        {
            foreach (var job in _jobs)
            {
                foreach (var task in job.Tasks)
                {
                    try
                    {
                        task.Execute();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Task execution failed. message: {message}", ex.Message);
                        throw;
                    }
                }
            }
        }
    }
    
    private void BuildAndStartMode(UserSettings settings)
    {
        _currentMode = _modeFactory.GetExecutionMode();
        _timer.Start();

        _logger.LogInformation("Scheduler started, mode: {mode}", settings.ExecutionMode);
    }
    
    private void OnUserSettingsChanged(UserSettings updated)
    {
        _logger.LogInformation("Scheduler changed, mode: {mode}", updated.ExecutionMode);
        
        _timer.Stop();
        
        _currentMode = _modeFactory.GetExecutionMode();
        
        _timer.Start();
    }
}