using DarkMode.Core.Domains;

namespace DarkMode.Core.Services.WorkMode;

public class CustomTimeMode : WorkModeBase
{
    public override Task<DateTime?> CalculateSwitchTimeAsync() => throw new NotImplementedException();

    public override bool CanExecute() => throw new NotImplementedException();
}