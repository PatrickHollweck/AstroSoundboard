// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 21:31
// Creation: 20:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SettingsView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Pages.Settings
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

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
            Model = new SettingsModel();

            InitializeComponent();
            DataContext = Model;

            ColorBox.SelectedValue = Model.SelectedColor;
        }

        private void ChangePrimaryColor(object sender, SelectionChangedEventArgs e) => Model.SelectedColor = ColorBox.SelectedIndex;

        private void ChangeAccentColor(object sender, SelectionChangedEventArgs e) => Model.SelectedAccentColor = AccentColorBox.SelectedIndex;

        private void ChangeLightMode(object sender, RoutedEventArgs e) => Model.IsDarkModeEnabled = !Settings.Default.IsDarkModeEnabled;

        private void BrowserChangeLog(object sender, RoutedEventArgs e) => Process.Start("https://github.com/FetzenRndy/AstroSoundboard/releases/");

        private void OpenApplicationPath_Click(object sender, RoutedEventArgs e) => Process.Start("explorer.exe", AppSettings.AssemblyDirectory);

        private void OpenLogsFolder(object sender, RoutedEventArgs e) => Process.Start("explorer.exe", @"C:\ProgramData\AstroKittySoundBoard\logs");

        private void GiveFeedback_Click(object sender, RoutedEventArgs e) => new FeedbackWindow().Show();

        private void EnableKeybindsToogle(object sender, RoutedEventArgs e) => Model.EnableKeybinds = !Settings.Default.EnableSoundHotKeys;

        private void Uninstall(object sender, RoutedEventArgs e)
        {
            try
            {
                Directory.Delete(@"C:\ProgramData\AstroKittySoundBoard", true);
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