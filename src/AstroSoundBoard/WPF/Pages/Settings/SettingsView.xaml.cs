// ****************************** Module Header ****************************** //
//
//
// Last Modified: 08:05:2017 / 18:27
// Creation: 08:05:2017
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
    using AstroSoundBoard.WPF.Windows;

    using log4net;

    public partial class SettingsView : UserControl
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private SettingsModel Model { get; set; }

        public SettingsView()
        {
            Model = new SettingsModel
            {
                EnableKeybinds = Settings.Default.EnableSoundHotKeys,
                IsDarkModeEnabled = Settings.Default.IsDarkModeEnabled,
                SelectedColor = Settings.Default.PrimaryColor
            };

            InitializeComponent();
            DataContext = Model;

            ColorBox.SelectedValue = Settings.Default.PrimaryColor;
            AllowErrorReportingToogleButton.IsChecked = Settings.Default.AllowErrorReporting;
        }

        private void ChangePrimaryColor(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.PrimaryColor = ColorBox.SelectedIndex;
            Settings.Default.Save();

            App.ApplyMaterialTheme();
        }

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

        private void RequestImplementation_Click(object sender, RoutedEventArgs e) => new FeedbackWindow().Show();

        private void ReportIssue_Click(object sender, RoutedEventArgs e) => new FeedbackWindow().Show();

        private void AllowErrorReportingToogleButton_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.AllowErrorReporting)
            {
                Settings.Default.AllowErrorReporting = false;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.AllowErrorReporting = true;
                Settings.Default.Save();
            }

            Log.Info($"Changed ErrorReporting! TO: {Settings.Default.AllowErrorReporting}");
        }

        private void EnableKeybindsToogle(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.EnableSoundHotKeys)
            {
                Settings.Default.EnableSoundHotKeys = false;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.EnableSoundHotKeys = true;
                Settings.Default.Save();
            }
        }
    }
}