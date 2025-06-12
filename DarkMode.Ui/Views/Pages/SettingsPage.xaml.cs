using System.Windows.Controls;
using DarkMode.Ui.ViewModels.Pages;

namespace DarkMode.Ui.Views.Pages;

public partial class SettingsPage : Page
{
    public SettingsPageViewModel ViewModel { get; }
    public SettingsPage(SettingsPageViewModel viewModel)
    {
        ViewModel = viewModel;
        
        InitializeComponent();
    }
}