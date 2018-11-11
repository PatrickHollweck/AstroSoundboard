// ****************************** Module Header ****************************** //
//
//
// Last Modified: 17:07:2017 / 17:09
// Creation: 20:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SettingsView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using AstroSoundBoard.Core.Objects;
using AstroSoundBoard.WPF.Windows;

namespace AstroSoundBoard.WPF.Pages.Settings
{
    public partial class SettingsView : UserControl
    {
        private SettingsModel Model { get; }

        public SettingsView()
        {
            Model = new SettingsModel();

            InitializeComponent();
            DataContext = Model;

            ColorBox.SelectedValue = Model.SelectedColor;
        }

        private void ChangePrimaryColor(object sender, SelectionChangedEventArgs e) => Model.SelectedColor = ColorBox.SelectedIndex;

        private void ChangeAccentColor(object sender, SelectionChangedEventArgs e) => Model.SelectedAccentColor = AccentColorBox.SelectedIndex;

        private void ChangeLightMode(object sender, RoutedEventArgs e) => Model.IsDarkModeEnabled = !Properties.Settings.Default.IsDarkModeEnabled;

        private void BrowserChangeLog(object sender, RoutedEventArgs e) => Process.Start($"{Properties.Resources.Project_Github}/releases");

        private void OpenApplicationPath_Click(object sender, RoutedEventArgs e) => Process.Start("explorer.exe", AppSettings.AssemblyDirectory);

        private void OpenLogsFolder(object sender, RoutedEventArgs e) => Process.Start("explorer.exe", @"C:\ProgramData\AstroKittySoundBoard\logs");

        private void GiveFeedback_Click(object sender, RoutedEventArgs e) => new FeedbackWindow().Show();

        private void EnableKeybindsToogle(object sender, RoutedEventArgs e) => Model.EnableKeybinds = !Properties.Settings.Default.EnableSoundHotKeys;

        private void ResetSoundboard(object sender, RoutedEventArgs e)
        {
            try
            {
                Directory.Delete(AppSettings.SettingsFilePath, true);

                Properties.Settings.Default.IsDarkModeEnabled = true;
                Properties.Settings.Default.PrimaryColor = 3;
                Properties.Settings.Default.AccentColor = 2;
                Properties.Settings.Default.AllowErrorReporting = true;
                Properties.Settings.Default.EnableSoundHotKeys = true;
                Properties.Settings.Default.Volume = 100;

                Properties.Settings.Default.Save();
            }
            catch
            {
                // Eat
            }
            finally
            {
                MessageBox.Show("Uninstall complete, to finish the uninstall delete the .exe file you started the Program from. Bye :3", "Uninstall", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}