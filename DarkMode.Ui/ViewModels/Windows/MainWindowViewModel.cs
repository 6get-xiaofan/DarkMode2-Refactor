using DarkMode.Ui.Services;
using DarkMode.Ui.Views.Pages;
using DarkMode.Ui.Views.Windows;

namespace DarkMode.Ui.ViewModels.Windows;

public partial class MainWindowViewModel(WindowsProviderService windowsProviderService)
{
    [RelayCommand]
    public void OpenSettingsWindow()
    {
        windowsProviderService.Show<SettingsWindow>(typeof(SetTimePage));
    }

    [RelayCommand]
    public void ExitProgram()
    {
        Application.Current.Shutdown();
    }
}