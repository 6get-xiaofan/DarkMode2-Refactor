using DarkMode.Core.Constants;
using DarkMode.Core.Interfaces.Config;
using DarkMode.Core.Models;

namespace DarkMode.Ui.ViewModels.Pages;

public partial class SettingsPageViewModel(IConfigService service) : ViewModel
{
    [ObservableProperty]
    private bool _isDeveloperModeEnabled;
    
    [RelayCommand]
    private void SwitchDeveloperMode()
    {
        if (_isDeveloperModeEnabled)
        {
            var settings = new AppSettings
            {
                DeveloperMode = true
            };
            service.SaveConfig(PathConstants.AppSettingsPath, settings);
        }
        else
        {
            var settings = new AppSettings
            {
                DeveloperMode = false
            };
            service.SaveConfig(PathConstants.AppSettingsPath, settings);
        }
    }
}