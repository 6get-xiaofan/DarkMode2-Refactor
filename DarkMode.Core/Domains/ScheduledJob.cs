using DarkMode.Core.Interfaces.Condition;
using DarkMode.Core.Interfaces.Task;

namespace DarkMode.Core.Domains;

public class ScheduledJob
{
    public string Name { get; set; }
    public List<ITask> Tasks { get; set; } = new List<ITask>();
}