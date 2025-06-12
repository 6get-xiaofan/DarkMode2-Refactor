namespace DarkMode.Core.Events;

public class TaskExecutingEventArgs : EventArgs
{
    public string TaskName { get; set; }
    public DateTime StartTime { get; set; }
}