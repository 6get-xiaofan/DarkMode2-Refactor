using DarkMode.Core.Enums;

namespace DarkMode.Core.Interfaces.Execution;

public interface IExecutionMode
{
    ExecutionType Name { get; }
    bool ShouldExecute();
}