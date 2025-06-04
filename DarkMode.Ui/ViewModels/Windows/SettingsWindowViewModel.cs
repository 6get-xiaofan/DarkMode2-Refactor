using DarkMode.Ui.I18n;
using DarkMode.Ui.Views.Pages;
using Microsoft.Extensions.Localization;
using Wpf.Ui.Controls;

namespace DarkMode.Ui.ViewModels.Windows;

public partial class SettingsWindowViewModel(IStringLocalizer localizer) : ViewModel
{
    
    [ObservableProperty] 
    private ObservableCollection<object> _menuItems =
    [
        new NavigationViewItem()
        {
            Content = new TextBlock()
            {
                Text = localizer["SettingsWindow.NavigationItem1"].Value,
                FontSize = 11
            },
            Icon = new FontIcon()
            {
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"),"./Assets/Fonts/#FluentSystemIcons-Regular"),
                Glyph = "\uf823",
                FontSize = 26,
            },
            TargetPageType = typeof(SetTimePage)
        },
        new NavigationViewItem()
        {
            Content = new TextBlock()
            {
                Text = localizer["SettingsWindow.NavigationItem2"].Value,
                FontSize = 11
            },
            Icon = new FontIcon()
            {
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"),"./Assets/Fonts/#FluentSystemIcons-Regular"),
                Glyph = "\uf867",
                FontSize = 26,
            },
            TargetPageType = typeof(SetWallpaperPage)
        },
        new NavigationViewItem()
        {
            Content = new TextBlock()
            {
                Text = localizer["SettingsWindow.NavigationItem3"].Value,
                FontSize = 11
            },
            Icon = new FontIcon()
            {
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"),"./Assets/Fonts/#FluentSystemIcons-Regular"),
                Glyph = "\ue8d8",
                FontSize = 26,
            },
            TargetPageType = typeof(CustomPage)
        },
        new NavigationViewItem()
        {
            Content = new TextBlock()
            {
                Text = localizer["SettingsWindow.NavigationItem4"].Value,
                FontSize = 11
            },
            Icon = new FontIcon()
            {
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"),"./Assets/Fonts/#FluentSystemIcons-Regular"),
                Glyph = "\uf83b",
                FontSize = 26,
            },
            TargetPageType = typeof(LaboratoryPage)
        },
    ];
    
    [ObservableProperty]
    private ObservableCollection<object> _footerMenuItems =
    [
        new NavigationViewItem()
        {
            Content = new TextBlock()
            {
                Text = localizer["SettingsWindow.NavigationItem5"].Value,
                FontSize = 11
            },
            Icon = new FontIcon()
            {
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"),"./Assets/Fonts/#FluentSystemIcons-Regular"),
                Glyph = "\uf2f0",
                FontSize = 26,
            },
            TargetPageType = typeof(DeveloperModePage)
        },
        new NavigationViewItem()
        {
            Content = new TextBlock()
            {
                Text = localizer["SettingsWindow.NavigationItem6"].Value,
                FontSize = 11
            },
            Icon = new FontIcon()
            {
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"),"./Assets/Fonts/#FluentSystemIcons-Regular"),
                Glyph = "\uf4a4",
                FontSize = 26,
            },
            TargetPageType = typeof(AboutPage)
        },
        new NavigationViewItem()
        {
            Content = new TextBlock()
            {
                Text = localizer["SettingsWindow.NavigationItem7"].Value,
                FontSize = 11
            },
            Icon = new FontIcon()
            {
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"),"./Assets/Fonts/#FluentSystemIcons-Regular"),
                Glyph = "\uf6aa",
                FontSize = 26,
            },
            TargetPageType = typeof(SettingsPage)
        }
    ];
}