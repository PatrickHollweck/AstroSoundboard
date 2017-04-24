// ****************************** Module Header ****************************** //
//
//
// Last Modified: 23:04:2017 / 15:18
// Creation: 23:04:2017
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

    public class AppSettings
    {
        /// <summary>
        /// Installation Path Constant.
        /// </summary>
        public const string InstallationFilePath = @"C:/ProgramData/AstroKittySoundBoard";

        /// <summary>
        /// Path to the /soundSettings.json file.
        /// </summary>
        public static readonly string SoundSettingsFilePath = $"{InstallationFilePath}/soundSettings.json";

        /// <summary>
        /// File path to the Definition Definition file.
        /// </summary>
        public static string SoundDefinitionFilePath { get; } = $"{InstallationFilePath}/SoundDef.json";

        /// <summary>
        /// Get the Directory the Assembly is in.
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder builder = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(builder.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}