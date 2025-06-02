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
            Name = localizer["SettingsWindow.NavigationItem1"].Value,
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
            TargetPageType = typeof(SetTimePage),
            TargetPageTag = "时间"
        },
        new NavigationViewItem()
        {
            Name = localizer["SettingsWindow.NavigationItem2"].Value,
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
            TargetPageType = typeof(SetWallpaperPage),
            TargetPageTag = "壁纸"
        },
        new NavigationViewItem()
        {
            Name = localizer["SettingsWindow.NavigationItem3"].Value,
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
            TargetPageType = typeof(CustomPage),
            TargetPageTag = "自定义功能"
        },
        new NavigationViewItem()
        {
            Name = localizer["SettingsWindow.NavigationItem4"].Value,
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
            TargetPageType = typeof(LaboratoryPage),
            TargetPageTag = "实验室"
        },
    ];
    
    [ObservableProperty]
    private ObservableCollection<object> _footerMenuItems =
    [
        new NavigationViewItem()
        {
            Name = localizer["SettingsWindow.NavigationItem5"].Value,
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
            TargetPageType = typeof(DeveloperModePage),
            TargetPageTag = "开发者选项"
        },
        new NavigationViewItem()
        {
            Name = localizer["SettingsWindow.NavigationItem6"].Value,
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
            TargetPageType = typeof(AboutPage),
            TargetPageTag = "关于"
        },
        new NavigationViewItem()
        {
            Name = localizer["SettingsWindow.NavigationItem7"].Value,
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
            TargetPageType = typeof(SettingsPage),
            TargetPageTag = "程序设置"
        }
    ];
}