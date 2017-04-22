// ****************************** Module Header ****************************** //
//
//
// Last Modified: 22:04:2017 / 22:37
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

    using AstroSoundBoard.WPF.Windows;

    using log4net;

    using Squirrel;

    public class AppUpdater
    {
        static AppUpdater()
        {
            Start();
        }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly string GitHubLink = @"https://github.com/FetzenRndy/AstroSoundboard";

        public static void Start()
        {
            Task.Run(async () =>
                {
                    try
                    {
                        using (var manager = UpdateManager.GitHubUpdateManager(GitHubLink))
                        {
                            await manager.Result.UpdateApp().ConfigureAwait(false);

                            SquirrelAwareApp.HandleEvents
                            (
                                onAppUpdate: v =>
                                    {
                                        manager.Result.CreateShortcutForThisExe();

                                        var updateWindow = new UpdateWindow();
                                        updateWindow.Show();
                                    },
                                onFirstRun: () =>
                                    {
                                        manager.Result.CreateShortcutForThisExe();
                                        Log.Info("First Run!");
                                    },
                                onAppUninstall: v => { manager.Result.RemoveShortcutForThisExe(); },
                                onInitialInstall: v => { manager.Result.CreateShortcutForThisExe(); }
                            );
                        }
                    }
                    catch (Exception exception)
                    {
                        Log.Error("Can not update!", exception);
                    }
                });
        }
    }
}