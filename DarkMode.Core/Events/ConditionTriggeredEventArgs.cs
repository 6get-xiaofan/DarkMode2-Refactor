namespace DarkMode.Core.Events;

public class ConditionTriggeredEventArgs : EventArgs
{
    public string ConditionName { get; set; }
    public DateTime Timestamp { get; set; }
}