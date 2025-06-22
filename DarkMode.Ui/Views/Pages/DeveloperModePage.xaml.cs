using System.Windows.Controls;
using DarkMode.Ui.ViewModels.Pages;

namespace DarkMode.Ui.Views.Pages;

public partial class DeveloperModePage : INavigableView<DeveloperModePageViewModel>
{
    public DeveloperModePageViewModel ViewModel { get; }
    public DeveloperModePage(DeveloperModePageViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
        
        InitializeComponent();
        
        Unloaded += (s, e) => viewModel.Dispose();
    }
}