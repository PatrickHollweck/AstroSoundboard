// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 14:13
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SettingsModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Objects.Models
{
    using System.Reflection;

    using AstroSoundBoard.Properties;

    public class SettingsModel
    {
        private readonly Settings configuration = Settings.Default;

        private bool enableKeybinds = Settings.Default.EnableSoundHotKeys;
        public bool EnableKeybinds
        {
            get => enableKeybinds;
            set
            {
                enableKeybinds = configuration.EnableSoundHotKeys = value;
                configuration.Save();
            }
        }

        private bool isDarkModeEnabled = Settings.Default.IsDarkModeEnabled;
        public bool IsDarkModeEnabled
        {
            get => isDarkModeEnabled;
            set
            {
                isDarkModeEnabled = configuration.IsDarkModeEnabled = value;
                configuration.Save();
                Bootstrapper.ApplyMaterialTheme();
            }
        }

        private int selectedColor = Settings.Default.PrimaryColor;
        public int SelectedColor
        {
            get => selectedColor;
            set
            {
                selectedColor = configuration.PrimaryColor = value;
                configuration.Save();
                Bootstrapper.ApplyMaterialTheme();
            }
        }

        private int selectedAccentColor = Settings.Default.AccentColor;
        public int SelectedAccentColor
        {
            get => selectedAccentColor;
            set
            {
                selectedAccentColor = configuration.AccentColor = value;
                configuration.Save();
                Bootstrapper.ApplyMaterialTheme();
            }
        }

        public string CurrentVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}