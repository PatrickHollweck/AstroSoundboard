// ****************************** Module Header ****************************** //
//
//
// Last Modified: 01:05:2017 / 01:21
// Creation: 29:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="App.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects;
    using AstroSoundBoard.Core.Utils;
    using AstroSoundBoard.Properties;

    using log4net;
    using log4net.Core;
    using log4net.Repository.Hierarchy;

    using MaterialDesignThemes.Wpf;

    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
#if DEBUG
            ((Hierarchy)LogManager.GetRepository()).Root.Level = Level.Debug;
            ((Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
#else
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = Level.Warn;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
#endif

            Log.Info("--- APP START! ---");
            Log.Info($"Current Version: {Assembly.GetExecutingAssembly().GetName().Version}");

            // Make sure all required Folders exist.
            FileSystem.FolderHelper.CreateIfMissing($"{AppSettings.InstallationFilePath}/");

            // Setup error handling to log fatal errors.
            AppDomain.CurrentDomain.UnhandledException += (caller, args) =>
                {
                    Log.Fatal($"Fatal unhanded exception. - {args.ExceptionObject} -- {args.IsTerminating} -> {args}");

#if !DEBUG
                    if (AstroSoundBoard.Properties.Settings.Default.AllowErrorReporting)
                    {
                        var ravenClient = new RavenClient(Credentials.SentryApiKey);
                        ravenClient.Capture(new SharpRaven.Data.SentryEvent((Exception)args.ExceptionObject));

                        Log.Info("Reported error to sentry!");
                    }

#endif
                };

            ApplyMaterialTheme();
            SoundManager.Init();
            SettingsManager.Init();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Log.Info("--- APP EXIT! ---");
        }

        public static void ApplyMaterialTheme()
        {
            List<string> colorList = new List<string> { "Red", "Pink", "Purple", "Indigo", "Blue", "Cyan", "Teal", "Green", "Lime", "Yellow", "Amber", "Orange", "Brown", "Grey" };

            var palette = new PaletteHelper();
            palette.SetLightDark(Settings.Default.IsDarkModeEnabled);
            palette.ReplacePrimaryColor(colorList[Settings.Default.PrimaryColor]);
        }
    }
}