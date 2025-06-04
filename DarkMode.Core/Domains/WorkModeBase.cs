namespace DarkMode.Core.Domains;

public abstract class WorkModeBase
{
    public abstract Task<DateTime?> CalculateSwitchTimeAsync();
    public abstract bool CanExecute();
}