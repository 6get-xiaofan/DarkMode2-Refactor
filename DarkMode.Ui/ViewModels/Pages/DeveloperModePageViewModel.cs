using DarkMode.Core.Constants;
using DarkMode.Core.Interfaces.Config;
using DarkMode.Core.Interfaces.Hardware;
using DarkMode.Core.Models;
using DarkMode.Core.Services.Config;
using DarkMode.Ui.Services;
using Lepo.i18n;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace DarkMode.Ui.ViewModels.Pages;

public partial class DeveloperModePageViewModel : ViewModel
{
    private ISnackbarService _snackbarService;
    private IContentDialogService _dialogService;
    private IHardwareService _hardwareService;
    
    private DispatcherTimer timer;
    
    private readonly ILocalizationCultureManager _cultureManager;
    private readonly IConfigService _service;
    public DeveloperModePageViewModel(
        ISnackbarService snackbarService, 
        IContentDialogService contentDialogService, 
        IHardwareService hardwareService,
        ILocalizationCultureManager cultureManager,
        IConfigService service)
    {
        _snackbarService = snackbarService;
        _dialogService = contentDialogService;
        _hardwareService = hardwareService;
        _cultureManager = cultureManager;
        _service = service;
        
        InitializeMonitoring();
    }
    
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
        _snackbarService.Show(
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
        ContentDialogResult result = await _dialogService.ShowSimpleDialogAsync(
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
    
    [ObservableProperty]
    private float _gpuUsage;
    
    // 初始化监控定时器
    private void InitializeMonitoring()
    {
        timer = new DispatcherTimer 
        {
            Interval = TimeSpan.FromSeconds(1) // 每秒更新
        };
        timer.Tick += async (s, e) => await RefreshDataAsync();
        timer.Start();
    }

    // 异步刷新数据
    private async Task RefreshDataAsync()
    {
        GpuUsage = await Task.Run(() => _hardwareService.GetGpuUsage());
    }

    // 清理资源
    public void Dispose() => timer?.Stop();

    [RelayCommand]
    private async Task TestException()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private void SwitchEnglish()
    {
        _cultureManager.SetCulture("en-US");
        
        var settings = new AppSettings
        {
            Language = "en-US"
        };
        _service.SaveConfig(PathConstants.AppSettingsPath, settings);
        
        Process.Start(Process.GetCurrentProcess().ProcessName + ".exe");
        App.Current.Shutdown();
    }
    
    [RelayCommand]
    private void SwitchChines()
    {
        _cultureManager.SetCulture("zh-CN");
        
        var settings = new AppSettings
        {
            Language = "zh-CN"
        };
        
        _service.SaveConfig(PathConstants.AppSettingsPath, settings);
        Process.Start(Process.GetCurrentProcess().ProcessName + ".exe");
        App.Current.Shutdown();
    }
}