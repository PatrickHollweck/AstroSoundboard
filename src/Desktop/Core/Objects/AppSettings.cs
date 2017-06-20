// ****************************** Module Header ****************************** //
//
//
// Last Modified: 08:05:2017 / 17:57
// Creation: 08:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="AppSettings.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Objects
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// App level Constants
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Installation Path Constant.
        /// </summary>
        public const string InstallationFilePath = @"C:/ProgramData/AstroKittySoundBoard";

        /// <summary>
        /// Path to the /soundSettings.json file.
        /// </summary>
        public static string SoundSettingsFilePath { get; } = $"{InstallationFilePath}/soundSettings.json";

        /// <summary>
        /// Get the Directory the Assembly is in.
        /// </summary>
        public static string AssemblyDirectory => Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
    }
}