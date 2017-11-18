// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:07:2017 / 18:44
// Creation: 20:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="AppSettings.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Objects
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
        /// Static Random.
        /// </summary>
        public static Random Rng = new Random();

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