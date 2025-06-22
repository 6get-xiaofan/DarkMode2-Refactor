using DarkMode.Core.Constants;
using DarkMode.Core.Interfaces.Config;
using DarkMode.Core.Models;
using DarkMode.Ui.Resources;
using Lepo.i18n;
using Microsoft.Extensions.Logging;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace DarkMode.Ui.ViewModels.Pages;

public partial class SettingsPageViewModel: ViewModel
{
    private readonly IConfigService _configService;
    private readonly ILogger<SettingsPageViewModel> _logger;
    private readonly ISnackbarService _snackbarService;
    private readonly ILocalizationCultureManager _cultureManager;
    
    private bool _isInitializing = false;
    
    public SettingsPageViewModel(
        IConfigService configService,
        ILogger<SettingsPageViewModel> logger,
        ISnackbarService snackbarService,
        ILocalizationCultureManager cultureManager
    )
    {
        _configService = configService;
        _logger = logger;
        _snackbarService = snackbarService;
        _cultureManager = cultureManager;

        InitiationOption();
    }

    private void InitiationOption()
    {
        var config = _configService.LoadConfig<AppSettings>(PathConstants.AppSettingsPath);
        
        // Language
        var languageCode = config.Language;
        
        SelectedLanguage = SupportedLanguages.FirstOrDefault(lang => lang.LanguageCode == languageCode)
                           ?? SupportedLanguages.First();
        
        // Developer Mode
        IsDeveloperModeEnabled = config.DeveloperMode;
        
        _isInitializing = true;
    }

    [ObservableProperty] private SupportedLanguage _selectedLanguage;
    
    [ObservableProperty]
    private ObservableCollection<SupportedLanguage> _supportedLanguages = new ()
    {
        new SupportedLanguage { LanguageName = "简体中文 (中国)", LanguageCode = "zh-CN" },
        new SupportedLanguage { LanguageName = "繁體中文 (中國台灣)", LanguageCode = "zh-TW" },
        new SupportedLanguage { LanguageName = "English (United States)", LanguageCode = "en-US" }
    };
    
    partial void OnSelectedLanguageChanged(SupportedLanguage value)
    {
        if (!_isInitializing) return;
        
        _logger.LogInformation($"Change Language: {value}");
        _cultureManager.SetCulture(new CultureInfo(value.LanguageCode));
        
        var settings = new AppSettings
        {
            Language = value.LanguageCode
        };
        
        _configService.SaveConfig(PathConstants.AppSettingsPath, settings);
        Process.Start(Process.GetCurrentProcess().ProcessName + ".exe");
        App.Current.Shutdown();
    }

    [ObservableProperty] private bool _isDeveloperModeEnabled;

    partial void OnIsDeveloperModeEnabledChanged(bool value)
    {
        if (!_isInitializing) return;
        
        _logger.LogDebug($"IsDeveloperModeEnabled: {value}");

        var settings = new AppSettings { DeveloperMode = value };
        _configService.SaveConfig(PathConstants.AppSettingsPath, settings);

        _snackbarService.Show(
            "提示",
            "重启后生效",
            ControlAppearance.Secondary,
            new SymbolIcon(SymbolRegular.Info24),
            TimeSpan.FromSeconds(2)
        );
    }
}