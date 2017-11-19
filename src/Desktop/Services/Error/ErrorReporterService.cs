// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 18:33
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="ErrorReporterService.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Error
{
    using System;

    public class ErrorReporterService
    {
        public void Report(IErrorReporter reporter, Exception error)
        {
            reporter.Report(error);
        }

        /// <summary>
        /// Attaches a Error Handler in Debug and Release Mode.
        /// </summary>
        /// <param name="reporter">A Reporter</param>
        public static void AttachErrorHandler(IErrorReporter reporter)
        {
            AppDomain.CurrentDomain.UnhandledException += reporter.Report;
        }

        /// <summary>
        /// Attaches a Error Handler only in Debug Mode.
        /// </summary>
        /// <param name="reporter">A Reporter</param>
        public static void AttatchDebugErrorHandler(IErrorReporter reporter)
        {
#if DEBUG
            AppDomain.CurrentDomain.UnhandledException += reporter.Report;
#endif
        }

        /// <summary>
        /// Attaches a Error Handler only in Release Mode.
        /// </summary>
        /// <param name="reporter">A Reporter</param>
        public static void AttatchProductionErrorHandler(IErrorReporter reporter)
        {
#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += reporter.Report;
#endif
        }
    }
}