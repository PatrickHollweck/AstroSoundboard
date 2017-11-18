// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 14:25
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="Bootstrapper.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;

    using AstroSoundBoard.Misc;
    using AstroSoundBoard.Objects;
    using AstroSoundBoard.Properties;
    using AstroSoundBoard.Services;
    using AstroSoundBoard.ViewModels;

    using Caliburn.Micro;

    using CrashReporterDotNET;

    using log4net.Core;
    using log4net.Repository.Hierarchy;

    using MaterialDesignThemes.Wpf;

    using SharpRaven;

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
#if DEBUG
            ((Hierarchy)log4net.LogManager.GetRepository()).Root.Level = Level.Debug;
            ((Hierarchy)log4net.LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
#else
            ((Hierarchy)LogManager.GetRepository()).Root.Level = Level.Warn;
            ((Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);

            // Setup error handler
            AppDomain.CurrentDomain.UnhandledException += ReportError;
#endif

            Log.Info("--- APP START! ---");
            Log.Info($"Current Version: {Assembly.GetExecutingAssembly().GetName().Version}");

            FileSystem.FolderHelper.CreateIfMissing($"{AppSettings.InstallationFilePath}/");

            ApplyMaterialTheme();
            SoundManager.Init();
            SettingsManager.Init();

            DisplayRootViewFor<ShellViewModel>();
            ViewChanger.ChangeViewTo(ViewChanger.Page.Board);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            Log.Info("--- APP EXIT! ---");
        }

        // TODO: Refactor this.
        public static void ReportError(object sender, UnhandledExceptionEventArgs args)
        {
            Log.Fatal($"Fatal unhanded exception. (caught in ReportError Handler) - {args.ExceptionObject}");

            if (Settings.Default.AllowErrorReporting)
            {
                // Sentry
                var ravenClient = new RavenClient(Credentials.SentryApiKey);
                ravenClient.Capture(new SharpRaven.Data.SentryEvent((Exception)args.ExceptionObject));

                // CrashReporter.NET
                var reportCrash = new ReportCrash("patrick-hollweck@gmx.de")
                {
                    IncludeScreenshot = true,
                    CaptureScreen = true
                };

                reportCrash.Send((Exception)args.ExceptionObject);
            }
        }

        public static void ApplyMaterialTheme()
        {
            var colorList = new List<string> { "Red", "Pink", "Purple", "Indigo", "Blue", "Cyan", "Teal", "Green", "Lime", "Yellow", "Amber", "Orange", "Brown", "Grey" };

            var palette = new PaletteHelper();
            palette.SetLightDark(Settings.Default.IsDarkModeEnabled);
            palette.ReplaceAccentColor(colorList[Settings.Default.AccentColor]);
            palette.ReplacePrimaryColor(colorList[Settings.Default.PrimaryColor]);
        }
    }
}