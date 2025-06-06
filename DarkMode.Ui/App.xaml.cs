﻿using DarkMode.Core.Interfaces.Logging;
using DarkMode.Core.Services.Logging;
using DarkMode.Ui.Services;
using DarkMode.Ui.Services.Contracts;
using DarkMode.Ui.ViewModels.Pages;
using DarkMode.Ui.ViewModels.Windows;
using DarkMode.Ui.Views.Pages;
using DarkMode.Ui.Views.Windows;
using Lepo.i18n.DependencyInjection;
using Lepo.i18n.Json;
using Wpf.Ui;
using Wpf.Ui.DependencyInjection;

namespace DarkMode.Ui;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private static readonly IHost _host = Host
        .CreateDefaultBuilder()
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
            });
            
            // Logging
            _ = services.AddSingleton<ILoggerService, LoggerService>();
        }).Build();
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();
        var logger = _host.Services.GetRequiredService<ILoggerService>();
        logger.Info("UI", "DarkMode.Ui", nameof(App), nameof(App_OnStartup), "DarkMode starting...");
    }

    private void App_OnExit(object sender, ExitEventArgs e)
    {
        var logger = _host.Services.GetRequiredService<ILoggerService>();

        _host.StopAsync().Wait();
        logger.Info("UI", "DarkMode.Ui", nameof(App), nameof(App_OnStartup), "DarkMode exiting...");
        
        _host.Dispose();
    }

    private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        var logger = _host.Services.GetRequiredService<ILoggerService>();
        logger.Error("UI", "DarkMode.Ui", nameof(App), nameof(App_OnDispatcherUnhandledException), $"Unhandled exception: {e.Exception}");
        
        e.Handled = true;
    }
}