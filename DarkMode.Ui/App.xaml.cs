using DarkMode.Core.Constants;
using DarkMode.Core.Interfaces.Config;
using DarkMode.Core.Interfaces.Hardware;
using DarkMode.Core.Models;
using DarkMode.Core.Services.Config;
using DarkMode.Core.Services.Hardware;
using DarkMode.Core.Services.Logging;
using DarkMode.Ui.Services;
using DarkMode.Ui.Services.Contracts;
using DarkMode.Ui.ViewModels.Pages;
using DarkMode.Ui.ViewModels.Windows;
using DarkMode.Ui.Views.Pages;
using DarkMode.Ui.Views.Windows;
using Lepo.i18n.DependencyInjection;
using Lepo.i18n.Json;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Wpf.Ui;
using Wpf.Ui.DependencyInjection;
using Wpf.Ui.Extensions;

namespace DarkMode.Ui;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private static readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureLogging(c =>
        {
            c.ClearProviders();
            // Logging
            var loggerService = LoggerService.CreateLogger();
            c.AddSerilog(loggerService);
        })
        .ConfigureAppConfiguration(c =>
        {
            _ = c.SetBasePath(AppContext.BaseDirectory);
        }).ConfigureServices((context, services) =>
        {
            _ = services.AddNavigationViewPageProvider();
            
            // App Host
            _ = services.AddHostedService<ApplicationHostService>();
            
            // Window container with navigation
            _ = services.AddSingleton<IWindow, SettingsWindow>();
            _ = services.AddSingleton<INavigationService, NavigationService>();
            _ = services.AddSingleton<ISnackbarService, SnackbarService>();
            _ = services.AddSingleton<IContentDialogService, ContentDialogService>();
            _ = services.AddSingleton<WindowsProviderService>();
            
            // Window
            _ = services.AddSingleton<MainWindow>();
            _ = services.AddSingleton<MainWindowViewModel>();
            _ = services.AddSingleton<SettingsWindow>();
            _ = services.AddSingleton<SettingsWindowViewModel>();
            
            // Pages
            _ = services.AddTransient<SetTimePage>();
            _ = services.AddTransient<SetTimePageViewModel>();
            _ = services.AddTransient<SetWallpaperPage>();
            _ = services.AddTransient<SetWallpaperPageViewModel>();
            _ = services.AddTransient<CustomPage>();
            _ = services.AddTransient<CustomPageViewModel>();
            _ = services.AddTransient<LaboratoryPage>();
            _ = services.AddTransient<LaboratoryPageViewModel>();
            _ = services.AddTransient<DeveloperModePage>();
            _ = services.AddTransient<DeveloperModePageViewModel>();
            _ = services.AddTransient<AboutPage>();
            _ = services.AddTransient<AboutPageViewModel>();
            _ = services.AddTransient<SettingsPage>();
            _ = services.AddTransient<SettingsPageViewModel>();
            
            // I18n
            _ = services.AddStringLocalizer(b =>
            {
                _ = b.FromJson(Assembly.GetExecutingAssembly(), "Resources.Translations-zh-CN.json", new CultureInfo("zh-CN"));
                _ = b.FromJson(Assembly.GetExecutingAssembly(), "Resources.Translations-en-US.json", new CultureInfo("en-US"));
                var culture = new CultureInfo((JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(PathConstants.AppSettingsPath)).Language));
                b.SetCulture(culture);
            });
            
            // Other
            _ = services.AddSingleton<IHardwareService, HardwareService>();
            _ = services.AddSingleton<IConfigService, ConfigService>();
        }).Build();
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        // Single instance
        bool createdNew;
        _ = new Mutex(true, "DarkMode.Ui", out createdNew);
        if (!createdNew) Environment.Exit(0);
        
        // Start application
        _host.Start();
        
        var logger = _host.Services.GetRequiredService<ILogger<Application>>();
        logger.LogInformation("DarkMode starting...");
        
        // Init App
        var config = _host.Services.GetRequiredService<IConfigService>();
        config.EnsureValidConfig(PathConstants.AppSettingsPath, new AppSettings());
        config.EnsureValidConfig(PathConstants.UserSettingsPath, new UserSettings());

    }

    private void App_OnExit(object sender, ExitEventArgs e)
    {
        var logger = _host.Services.GetRequiredService<ILogger<Application>>();

        _host.StopAsync().Wait();
        logger.LogInformation("DarkMode exiting...");
        
        _host.Dispose();
    }

    private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        var logger = _host.Services.GetRequiredService<ILogger<Application>>();
        logger.LogError("Unhandled exception: {Message}", e.Exception.Message);
        
        var dialog = _host.Services.GetRequiredService<IContentDialogService>();
        dialog.ShowSimpleDialogAsync(
            new SimpleContentDialogCreateOptions()
            {
                Title = "Unhandled exception",
                Content = e.Exception,
                PrimaryButtonText = "Feedback",
                CloseButtonText = "Cancel"
            }
        );
        e.Handled = true;
    }
}