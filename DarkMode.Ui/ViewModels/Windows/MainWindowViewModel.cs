using DarkMode.Ui.Services;
using DarkMode.Ui.Views.Pages;
using DarkMode.Ui.Views.Windows;
using Microsoft.Extensions.Logging;

namespace DarkMode.Ui.ViewModels.Windows;

public partial class MainWindowViewModel(WindowsProviderService windowsProviderService, ILogger<MainWindowViewModel> logger)
{
    [RelayCommand]
    public void OpenSettingsWindow()
    {
        windowsProviderService.Show<SettingsWindow>(typeof(SetTimePage));
        logger.LogInformation("Settings window opened");
    }

    [RelayCommand]
    public void ExitProgram()
    {
        Application.Current.Shutdown();
    }
}