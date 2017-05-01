// ****************************** Module Header ****************************** //
//
//
// Last Modified: 01:05:2017 / 15:09
// Creation: 29:04:2017
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
        public string CurrentVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public SettingsView()
        {
            InitializeComponent();
            DataContext = this;

            ColorBox.SelectedValue = Settings.Default.PrimaryColor;
            AllowErrorReportingToogleButton.IsChecked = Settings.Default.AllowErrorReporting;
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

        private void RequestImplementation_Click(object sender, RoutedEventArgs e)
        {
            var window = new FeedbackWindow();
            window.Show();
        }

        private void ReportIssue_Click(object sender, RoutedEventArgs e)
        {
            var window = new FeedbackWindow();
            window.Show();
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