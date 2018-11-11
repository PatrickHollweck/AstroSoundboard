// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 21:28
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SettingsModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System.Reflection;

namespace AstroSoundBoard.WPF.Pages.Settings
{
    public class SettingsModel
    {
        private readonly Properties.Settings configuration = Properties.Settings.Default;

        private int selectedColor = Properties.Settings.Default.PrimaryColor;
        private int selectedAccentColor = Properties.Settings.Default.AccentColor;

        private bool isDarkModeEnabled = Properties.Settings.Default.IsDarkModeEnabled;
        private bool enableKeybinds = Properties.Settings.Default.EnableSoundHotKeys;

        public string CurrentVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public bool EnableKeybinds
        {
            get => enableKeybinds;
            set
            {
                enableKeybinds = configuration.EnableSoundHotKeys = value;
                configuration.Save();
            }
        }

        public bool IsDarkModeEnabled
        {
            get => isDarkModeEnabled;
            set
            {
                isDarkModeEnabled = configuration.IsDarkModeEnabled = value;
                configuration.Save();
                App.ApplyMaterialTheme();
            }
        }

        public int SelectedColor
        {
            get => selectedColor;
            set
            {
                selectedColor = configuration.PrimaryColor = value;
                configuration.Save();
                App.ApplyMaterialTheme();
            }
        }

        public int SelectedAccentColor
        {
            get => selectedAccentColor;
            set
            {
                selectedAccentColor = configuration.AccentColor = value;
                configuration.Save();
                App.ApplyMaterialTheme();
            }
        }
    }
}