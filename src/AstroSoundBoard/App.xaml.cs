// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:04:2017 / 20:35
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="App.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard
{
	using System.Windows;

	using log4net;

	public partial class App : Application
	{
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Log.Info("--- APP START! ---");
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			Log.Info("--- APP EXIT! ---");
		}
	}
}