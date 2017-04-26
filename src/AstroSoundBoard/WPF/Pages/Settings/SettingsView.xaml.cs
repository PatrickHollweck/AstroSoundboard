// ****************************** Module Header ****************************** //
//
//
// Last Modified: 26:04:2017 / 20:09
// Creation: 25:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SettingsView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Pages.Settings
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Objects;
    using AstroSoundBoard.Properties;

    public partial class SettingsView : UserControl
    {
        public string CurrentVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public SettingsView()
        {
            InitializeComponent();
            DataContext = this;

            ColorBox.SelectedValue = Settings.Default.PrimaryColor;
        }

        public int SelectedColor { get; set; } = Settings.Default.PrimaryColor;

        private void ChangePrimaryColor(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.PrimaryColor = ColorBox.SelectedIndex;
            Settings.Default.Save();

            App.ApplyMaterialTheme();
        }

        public bool IsDarkModeEnabled { get; set; } = Settings.Default.IsDarkModeEnabled;

        private void ChangeLightMode(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.IsDarkModeEnabled)
            {
                Settings.Default.IsDarkModeEnabled = false;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.IsDarkModeEnabled = true;
                Settings.Default.Save();
            }

            App.ApplyMaterialTheme();
        }

        private void BrowserChangeLog(object sender, RoutedEventArgs e) => Process.Start("https://github.com/FetzenRndy/AstroSoundboard/blob/master/public/changelog.md");

        private void OpenApplicationPath_Click(object sender, RoutedEventArgs e) => Process.Start("explorer.exe", AppSettings.AssemblyDirectory);
    }
}