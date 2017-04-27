// ****************************** Module Header ****************************** //
//
//
// Last Modified: 27:04:2017 / 16:52
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

    using AstroSoundBoard.Core.Components;
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

        private void ShowAbout_Click(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo(ViewChanger.Page.About);

        private void BrowserChangeLog(object sender, RoutedEventArgs e) => Process.Start("https://github.com/FetzenRndy/AstroSoundboard/releases/");

        private void OpenApplicationPath_Click(object sender, RoutedEventArgs e) => Process.Start("explorer.exe", AppSettings.AssemblyDirectory);

        private void OpenLogsFolder(object sender, RoutedEventArgs e) => Process.Start("explorer.exe", @"C:\ProgramData\AstroKittySoundBoard\logs");
    }
}