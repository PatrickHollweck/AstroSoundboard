// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 18:38
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="ThemeService.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Theme
{
    using System.Collections.Generic;

    using AstroSoundBoard.Properties;

    using MaterialDesignThemes.Wpf;

    public class ThemeService
    {
        // TODO: Implement and make use of.
        public static void SetPrimaryColor()
        {
        }

        public static void SetAccentColor()
        {
        }

        public static void ApplyTheme()
        {
            var colorList = new List<string> { "Red", "Pink", "Purple", "Indigo", "Blue", "Cyan", "Teal", "Green", "Lime", "Yellow", "Amber", "Orange", "Brown", "Grey" };

            var palette = new PaletteHelper();
            palette.SetLightDark(Settings.Default.IsDarkModeEnabled);
            palette.ReplaceAccentColor(colorList[Settings.Default.AccentColor]);
            palette.ReplacePrimaryColor(colorList[Settings.Default.PrimaryColor]);
        }
    }
}