// ****************************** Module Header ****************************** //
//
//
// Last Modified: 08:05:2017 / 18:29
// Creation: 08:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SettingsModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Pages.Settings
{
    using System.Reflection;

    using AstroSoundBoard.Properties;

    public class SettingsModel
    {
        public bool EnableKeybinds { get; set; } = Settings.Default.EnableSoundHotKeys;

        public bool IsDarkModeEnabled { get; set; } = Settings.Default.IsDarkModeEnabled;

        public int SelectedColor { get; set; } = Settings.Default.PrimaryColor;

        public string CurrentVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}