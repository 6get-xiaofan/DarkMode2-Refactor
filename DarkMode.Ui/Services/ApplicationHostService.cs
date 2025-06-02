using DarkMode.Ui.Services.Contracts;
using DarkMode.Ui.Views.Pages;
using DarkMode.Ui.Views.Windows;

namespace DarkMode.Ui.Services;

public class ApplicationHostService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public ApplicationHostService(IServiceProvider serviceProvider)
    {
        // If you want, you can do something with these services at the beginning of loading the application.
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return HandleActivationAsync();
    }

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Creates main window during activation.
    /// </summary>
    private Task HandleActivationAsync()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        // if (Application.Current.Windows.OfType<MainWindow>().Any())
        // {
        //     return Task.CompletedTask;
        // }

        // IWindow mainWindow = _serviceProvider.GetRequiredService<IWindow>();
        // mainWindow.Loaded += OnMainWindowLoaded;
        // mainWindow?.Show();

        return Task.CompletedTask;
    }

    // private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
    // {
    //     if (sender is not MainWindow settingsWindow)
    //     {
    //         return;
    //     }
    //
    //     _ = settingsWindow.NavigationView.Navigate(typeof(SetTimePage));
    // }
}