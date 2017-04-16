// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:04:2017 / 23:17
// Creation: 16:04:2017
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
		/// Installation Path Constant
		/// </summary>
		public const string InstallationPath = @"C:/ProgramData/AstroKittySoundBoard";

		/// <summary>
		/// Path to the /soundSettings.json File
		/// </summary>
		public static readonly string SoundSettingsFilePath = $"{InstallationPath}/soundSettings.json";

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