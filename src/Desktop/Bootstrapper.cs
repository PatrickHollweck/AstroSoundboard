// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 18:35
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="Bootstrapper.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard
{
    using System;
    using System.Reflection;
    using System.Windows;

    using AstroSoundBoard.Misc;
    using AstroSoundBoard.Objects;
    using AstroSoundBoard.Services;
    using AstroSoundBoard.Services.Error;
    using AstroSoundBoard.Services.Logging;
    using AstroSoundBoard.Services.Theme;
    using AstroSoundBoard.ViewModels;

    using Caliburn.Micro;

    using ILog = log4net.ILog;

    internal class Bootstrapper : BootstrapperBase
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            // Log some info.
            Log.Info("--- APP START! ---");
            Log.Info($"Current Version: {Assembly.GetExecutingAssembly().GetName().Version}");

            // Setup error Handling
            ErrorReporterService.AttachErrorHandler(new LogReporter());
            ErrorReporterService.AttachErrorHandler(new CrashReporterDotNetReporter());
            ErrorReporterService.AttatchProductionErrorHandler(new SentryReporter());

            // Create some necessary Folders.
            FileSystem.FolderHelper.CreateIfMissing($"{AppSettings.InstallationFilePath}/");

            // Apply the material theme.
            ThemeService.ApplyTheme();

            // Apply logging settings.
            Logger.ApplyLoggerSettings();

            // TODO: Refactor.
            SoundManager.Init();
            SettingsManager.Init();

            // Display Root view.
            DisplayRootViewFor<ShellViewModel>();
            ViewChanger.ChangeViewTo(ViewChanger.Page.Board);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            Log.Info("--- APP EXIT! ---");
        }
    }
}