using DarkMode.Ui.ViewModels.Windows;
using Wpf.Ui.Appearance;

namespace DarkMode.Ui.Views.Windows;

public partial class MainWindow
{
    // public MainWindowViewModel ViewModel { get; }
    
    public MainWindow(MainWindowViewModel viewModel)
    {
        SystemThemeWatcher.Watch(this);
        
        // ViewModel = viewModel;
        DataContext = viewModel;
        
        InitializeComponent();
    }
}