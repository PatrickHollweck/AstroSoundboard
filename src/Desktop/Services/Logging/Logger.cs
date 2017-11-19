// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 18:36
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="Logger.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Logging
{
    using System;

    using log4net.Core;
    using log4net.Repository.Hierarchy;

    public class Logger
    {
        // TODO: Implement Logger!
        public static void ApplyLoggerSettings()
        {
#if DEBUG
            ((Hierarchy)log4net.LogManager.GetRepository()).Root.Level = Level.Debug;
            ((Hierarchy)log4net.LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
#else
            ((Hierarchy)LogManager.GetRepository()).Root.Level = Level.Warn;
            ((Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
#endif
        }
    }
}