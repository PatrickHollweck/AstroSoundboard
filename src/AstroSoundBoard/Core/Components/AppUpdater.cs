// ****************************** Module Header ****************************** //
//
//
// Last Modified: 24:04:2017 / 19:41
// Creation: 23:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="AppUpdater.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;

    using AutoUpdaterDotNET;

    using log4net;

    public class AppUpdater
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Update()
        {
            Log.Info("Updater Started...");

            try
            {
                AutoUpdater.Start("http://localhost/download/AutoUpdaterTest.xml");
            }
            catch (Exception exception)
            {
                Log.Error("Update Error!", exception);
            }
        }
    }
}