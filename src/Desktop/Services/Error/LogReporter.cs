// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 18:25
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="LogReporter.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Error
{
    using System;

    using log4net;

    public class LogReporter : IErrorReporter
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Report(Exception error)
        {
            Log.Fatal($"Fatal unhanded exception. (caught in ReportError Handler) - {error}");
        }

        public void Report(object sender, UnhandledExceptionEventArgs error)
        {
            Report((Exception)error.ExceptionObject);
        }
    }
}