// ****************************** Module Header ****************************** //
//
//
// Last Modified: 25:04:2017 / 16:56
// Creation: 23:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="AppUpdater.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;
    using System.Windows;

    using AutoUpdaterDotNET;

    using log4net;

    public class AppUpdater
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void SilentUpdater(UpdateInfoEventArgs args)
        {
            Log.Info("AutoUpdater Started...");

            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    var message = $"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. Do you want to update the application now?";
                    MessageBoxResult dialogResult = MessageBox.Show(message, @"Update Available", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    if (dialogResult.Equals(MessageBoxResult.Yes))
                    {
                        try
                        {
                            AutoUpdater.DownloadUpdate();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("Update Error! \n\n" + exception.Message, exception.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    MessageBox.Show("The Update is finished! Please restart the application!", "Update!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    Log.Info("No Update!");
                }
            }
            else
            {
                Log.Info("No Update info!");
            }
        }

        public static void VisualUpdater(UpdateInfoEventArgs args)
        {
            Log.Info("Visual Updater Started...");

            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    var message = $"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. Do you want to update the application now?";
                    MessageBoxResult dialogResult = MessageBox.Show(message, @"Update Available", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    if (dialogResult.Equals(MessageBoxResult.Yes))
                    {
                        try
                        {
                            AutoUpdater.DownloadUpdate();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("Update Error! \n\n" + exception.Message, exception.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    MessageBox.Show("The Update is finished! Please restart the application!", "Update!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    MessageBox.Show(@"There is no update available please try again later.", @"No update available", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show(@"There is a problem reaching update server please check your internet connection and try again later.", @"Update check failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}