// ****************************** Module Header ****************************** //
//
//
// Last Modified: 23:04:2017 / 17:15
// Creation: 23:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="AppUpdater.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;

    using log4net;

    using Squirrel;

    public class AppUpdater
	{
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		static AppUpdater()
		{
			Update();
		}

		public static async void Update()
		{
			Log.Info("Trying to Update!");

			try
			{
				using (var mgr = UpdateManager.GitHubUpdateManager(@"https://github.com/FetzenRndy/AstroSoundboard"))
				{
					// TODO: Expand on that https://github.com/WillsB3/WOAI-P3D-Installer/blob/0fd1af0a4a84da6f641326068eef380c0ff7755a/WOAI-P3D-Installer/WOAI-P3D-Installer/Main.cs#L33
					var updates = mgr.Result.UpdateApp();
					await updates;
				}
			}
			catch (Exception exception)
			{
				Log.Error("UPDATE ERROR!", exception);
			}

			Log.Info("Update Process Done!");
		}
	}
}