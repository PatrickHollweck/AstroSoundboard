// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 18:22
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="CrashReporterDotNetReporter.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //
namespace AstroSoundBoard.Services.Error
{
    using System;

    using AstroSoundBoard.Properties;

    using CrashReporterDotNET;

    public class CrashReporterDotNetReporter : IErrorReporter
    {
        public static void ReportToUser(Exception error)
        {
            if (Settings.Default.AllowErrorReporting)
            {
            }
        }

        public void Report(Exception error)
        {
            var reportCrash = new ReportCrash("patrick-hollweck@gmx.de")
            {
                IncludeScreenshot = true,
                CaptureScreen = true
            };

            reportCrash.Send(error);
        }

        public void Report(object sender, UnhandledExceptionEventArgs error)
        {
            Report((Exception)error.ExceptionObject);
        }
    }
}