// ****************************** Module Header ****************************** //
//
//
// Last Modified: 25:04:2017 / 18:53
// Creation: 25:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="MainWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Windows
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.WPF.Pages.Board;

    using AutoUpdaterDotNET;

    using log4net;

    public partial class MainWindow : Window
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindow()
        {
            AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
            AutoUpdater.Start("https://raw.githubusercontent.com/FetzenRndy/AstroSoundboard/master/public/versions/updaterInfo.xml");

            ViewChanger.MainWindowInstance = this;

            InitializeComponent();

            ViewChanger.ChangeViewTo(ViewChanger.Page.Board);

            VolumeSlider_ValueChanged(new object(), new RoutedPropertyChangedEventArgs<double>(0, 50));
            VolumeSlider.Value = 50;
        }

        #region Helpers

        private bool CanSearchItemsExecute()
        {
            if (ViewChanger.MainWindowInstance.DataContext is BoardView)
            {
                SearchBox.IsEnabled = true;
                FavoriteButton.IsEnabled = true;
                return true;
            }
            else
            {
                SearchBox.IsEnabled = false;
                FavoriteButton.IsEnabled = false;
                return false;
            }
        }

        #endregion Helpers

        #region Search

        public void SearchForItem(object sender, TextChangedEventArgs e)
        {
            BoardView.BoadViewInstance?.SearchForElement(SearchBox.Text, onlyFavoritesActive);
        }

        #endregion Search

        #region Favorites

        private bool onlyFavoritesActive;

        public void ToogleFavorites()
        {
            Log.Debug("Toggling Favorites!");

            if (!CanSearchItemsExecute())
            {
                return;
            }

            if (onlyFavoritesActive)
            {
                onlyFavoritesActive = false;
                BoardView.BoadViewInstance?.OnlyShowFavorites(onlyFavoritesActive);
            }
            else
            {
                onlyFavoritesActive = true;
                BoardView.BoadViewInstance?.OnlyShowFavorites(onlyFavoritesActive);
            }
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            ToogleFavorites();
        }

        private void ContentControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CanSearchItemsExecute();
        }

        #endregion Favorites

        #region VolumeSlider

        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int newVolume = ushort.MaxValue / 100 * (int)e.NewValue;
            uint newVolumeAllChannels = ((uint)newVolume & 0x0000ffff) | ((uint)newVolume << 16);

            waveOutSetVolume(IntPtr.Zero, newVolumeAllChannels);
        }

        #endregion VolumeSlider

        #region UI Events

        private void ShowHome_Click(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo(ViewChanger.Page.Board);

        private void ShowSettings_Click(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo(ViewChanger.Page.Settings);

        private void BrowserFaceBook_Click(object sender, RoutedEventArgs e) => Process.Start("https://www.facebook.com/TheAstronautKitty");

        private void BrowserTwitter_Click(object sender, RoutedEventArgs e) => Process.Start("https://twitter.com/AstroShitty");

        private void BrowserYoutube_Click(object sender, RoutedEventArgs e) => Process.Start("https://www.youtube.com/user/TheAstronautKitty");

        private void BrowserRequestFeature_Click(object sender, RoutedEventArgs e) => Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSc9JnTYAgQ2fbaSujj9-F3DsI6_BOXJGG7jsXNLD6Dqf11X9g/viewform?usp=sf_link");

        private void BrowserRequestSound_Click(object sender, RoutedEventArgs e) => Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSfVT7Jx-5n80LYFskIcjLsL3fb2RmI7XOXAS_2rtUFIuyYp8Q/viewform?usp=sf_link");

        private void BrowserReportIssue_Click(object sender, RoutedEventArgs e) => Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSdrhFCCVeKrbA56kiLpOqA5H1nszwA4gWimV1V6YOjXr5mC-A/viewform?usp=sf_link");

        private void BrowserGitHub_Click(object sender, RoutedEventArgs e) => Process.Start("https://github.com/FetzenRndy/AstroSoundboard");

        #endregion UI Events
    }
}