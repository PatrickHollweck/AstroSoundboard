// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:04:2017 / 23:44
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="App.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard
{
	using System;
	using System.Reflection;
	using System.Windows;

	using AstroSoundBoard.Core.Components;
	using AstroSoundBoard.Core.Objects;
	using AstroSoundBoard.Core.Utils;
	using AstroSoundBoard.WPF.Pages.Settings;

	using log4net;

	using MaterialDesignThemes.Wpf;

	public partial class App : Application
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Log.Info("--- APP START! ---");
			Log.Info($"Current Version: {Assembly.GetExecutingAssembly().GetName().Version}");

			// Make sure all required Folders exist.
			FileSystem.FolderHelper.CreateIfMissing($"{AppSettings.InstallationPath}/");
			FileSystem.FolderHelper.CreateIfMissing($"{AppSettings.InstallationPath}/content");

			// Setup error handling to log fatal errors.
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.UnhandledException += (caller, args) => { Log.Fatal($"Fatal unhanded exception. - {args.ExceptionObject} -- {args.IsTerminating} -> {args}"); };

			ApplyMaterialTheme();
			SettingsManager.Init();
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			Log.Info("--- APP EXIT! ---");
		}

		public static void ApplyMaterialTheme()
		{
			var palette = new PaletteHelper();
			palette.SetLightDark(AstroSoundBoard.Properties.Settings.Default.IsDarkModeEnabled);
			palette.ReplacePrimaryColor(SettingsView.ColorList[AstroSoundBoard.Properties.Settings.Default.PrimaryColor]);
		}
	}
}