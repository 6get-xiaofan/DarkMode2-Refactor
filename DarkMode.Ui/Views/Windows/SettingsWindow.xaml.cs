using System.Windows;
using DarkMode.Ui.Services.Contracts;
using DarkMode.Ui.ViewModels.Windows;
using DarkMode.Ui.Views.Pages;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace DarkMode.Ui.Views.Windows;

public partial class SettingsWindow : IWindow
{
    public SettingsWindowViewModel ViewModel { get; }
    
    public SettingsWindow(
        SettingsWindowViewModel viewModel,
        INavigationService navigationService,
        IServiceProvider serviceProvider,
        ISnackbarService snackbarService,
        IContentDialogService contentDialogService
        )
    {
        SystemThemeWatcher.Watch(this);
        
        ViewModel = viewModel;
        DataContext = this;
        
        InitializeComponent();
        
        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        navigationService.SetNavigationControl(NavigationView);
        contentDialogService.SetDialogHost(RootContentDialog);
    }

    private void SettingsWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        NavigationView.Navigate(typeof(SetTimePage));
    }
}