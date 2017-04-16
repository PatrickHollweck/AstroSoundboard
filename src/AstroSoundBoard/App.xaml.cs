// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:04:2017 / 22:51
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="App.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard
{
	using System.Windows;

	using AstroSoundBoard.WPF.Pages.Settings;

	using log4net;

	using MaterialDesignThemes.Wpf;

	public partial class App : Application
	{
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Log.Info("--- APP START! ---");

			ApplyMaterialTheme();
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