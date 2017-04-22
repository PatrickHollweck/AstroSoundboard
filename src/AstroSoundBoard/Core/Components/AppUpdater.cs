// ****************************** Module Header ****************************** //
//
//
// Last Modified: 22:04:2017 / 21:48
// Creation: 22:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="AppUpdater.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;
    using System.Threading.Tasks;

    using log4net;

    using Squirrel;

    public class AppUpdater
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Init()
        {
            // Wired :3
            Start().Start();
        }

        private static async Task Start()
        {
            try
            {
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/FetzenRndy/AstroSoundboard"))
                {
                    await mgr.Result.UpdateApp().ConfigureAwait(true);
                }
            }
            catch (Exception exception)
            {
                Log.Fatal("Can not update!", exception);
            }
        }
    }
}