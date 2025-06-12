using DarkMode.Core.Models;

namespace DarkMode.Core.Events;

public class TaskExecutedEventArgs : EventArgs
{
    public string TaskName { get; set; }
    public TaskResult Result { get; set; }
}