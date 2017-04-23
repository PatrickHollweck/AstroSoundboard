// ****************************** Module Header ****************************** //
//
//
// Last Modified: 23:04:2017 / 14:20
// Creation: 23:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="AppUpdater.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;
    using System.Threading.Tasks;

    using AstroSoundBoard.Core.Objects;

    using log4net;

    using Squirrel;

    public class AppUpdater
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Start()
        {
            Task.Run(async () =>
                {
                    try
                    {
                        Log.Info("Trying to Update!");
                        using (var manager = UpdateManager.GitHubUpdateManager(@"https://github.com/FetzenRndy/AstroSoundboard"))
                        {
                            await manager.Result.UpdateApp().ConfigureAwait(false);

                            SquirrelAwareApp.HandleEvents
                            (
                                onAppUpdate: v =>
                                    {
                                        manager.Result.CreateShortcutForThisExe();
                                        AppSettings.showUpdateWindow = true;
                                    },
                                onFirstRun: () =>
                                    {
                                        manager.Result.CreateUninstallerRegistryEntry();
                                        manager.Result.CreateShortcutForThisExe();
                                        Log.Info("First Run!");
                                    },
                                onAppUninstall: v =>
                                    {
                                        manager.Result.RemoveShortcutForThisExe();
                                        manager.Result.RemoveUninstallerRegistryEntry();
                                    },
                                onInitialInstall: v =>
                                    {
                                        manager.Result.CreateUninstallerRegistryEntry();
                                        manager.Result.CreateShortcutForThisExe();
                                        Log.Info("First Run!");
                                    }

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