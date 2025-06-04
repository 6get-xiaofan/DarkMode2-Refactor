using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace DarkMode.Ui.ViewModels.Pages;

public partial class DeveloperModePageViewModel(ISnackbarService snackbarService, IContentDialogService contentDialogService) : ViewModel
{
    private ControlAppearance _snackbarAppearance = ControlAppearance.Secondary;

    [ObservableProperty]
    private int _snackbarTimeout = 2;

    private int _snackbarAppearanceComboBoxSelectedIndex = 1;

    public int SnackbarAppearanceComboBoxSelectedIndex
    {
        get => _snackbarAppearanceComboBoxSelectedIndex;
        set
        {
            _ = SetProperty(ref _snackbarAppearanceComboBoxSelectedIndex, value);
            UpdateSnackbarAppearance(value);
        }
    }

    [RelayCommand]
    private void OnOpenSnackbar(object sender)
    {
        snackbarService.Show(
            "Don't Blame Yourself.",
            "No Witcher's Ever Died In His Bed.",
            _snackbarAppearance,
            new SymbolIcon(SymbolRegular.Fluent24),
            TimeSpan.FromSeconds(SnackbarTimeout)
        );
    }

    private void UpdateSnackbarAppearance(int appearanceIndex)
    {
        _snackbarAppearance = appearanceIndex switch
        {
            1 => ControlAppearance.Secondary,
            2 => ControlAppearance.Info,
            3 => ControlAppearance.Success,
            4 => ControlAppearance.Caution,
            5 => ControlAppearance.Danger,
            6 => ControlAppearance.Light,
            7 => ControlAppearance.Dark,
            8 => ControlAppearance.Transparent,
            _ => ControlAppearance.Primary,
        };
    }
    
    [ObservableProperty]
    private string _dialogResultText = string.Empty;
    
    [RelayCommand]
    private async Task OnShowDialog(object content)
    {
        ContentDialogResult result = await contentDialogService.ShowSimpleDialogAsync(
            new SimpleContentDialogCreateOptions()
            {
                Title = "Save your work?",
                Content = content,
                PrimaryButtonText = "Save",
                SecondaryButtonText = "Don't Save",
                CloseButtonText = "Cancel",
            }
        );

        DialogResultText = result switch
        {
            ContentDialogResult.Primary => "User saved their work",
            ContentDialogResult.Secondary => "User did not save their work",
            _ => "User cancelled the dialog",
        };
    }
}