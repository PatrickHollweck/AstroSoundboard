// ****************************** Module Header ****************************** //
//
//
// Last Modified: 08:05:2017 / 17:09
// Creation: 08:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="MainWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Windows
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Utils;
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

        protected override void OnClosing(CancelEventArgs e)
        {
            KeybindManager.RemoveAllKeybindMappings();
            base.OnClosing(e);
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
            BoardView.BoardViewInstance?.SearchForElement(SearchBox.Text, onlyFavoritesActive);
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
                BoardView.BoardViewInstance?.OnlyShowFavorites(onlyFavoritesActive);
            }
            else
            {
                onlyFavoritesActive = true;
                BoardView.BoardViewInstance?.OnlyShowFavorites(onlyFavoritesActive);
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

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int newVolume = ushort.MaxValue / 100 * (int)e.NewValue;
            uint newVolumeAllChannels = ((uint)newVolume & 0x0000ffff) | ((uint)newVolume << 16);

            NativeMethods.waveOutSetVolume(IntPtr.Zero, newVolumeAllChannels);
        }

        #endregion VolumeSlider

        #region UI Events

        private bool isMenuExpanded = true;

        private void ShowHome_Click(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo(ViewChanger.Page.Board);

        private void ShowSettings_Click(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo(ViewChanger.Page.Settings);

        private void BrowserFaceBook_Click(object sender, RoutedEventArgs e) => Process.Start("https://www.facebook.com/TheAstronautKitty");

        private void BrowserTwitter_Click(object sender, RoutedEventArgs e) => Process.Start("https://twitter.com/AstroShitty");

        private void BrowserYoutube_Click(object sender, RoutedEventArgs e) => Process.Start("https://www.youtube.com/user/TheAstronautKitty");

        private void OpenFeedback(object sender, RoutedEventArgs e) => new FeedbackWindow().Show();

        private void SwitchToAbout(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo(ViewChanger.Page.About);

        private void OpenKeybindManager(object sender, RoutedEventArgs e) => new KeybindManagerWindow().Show();

        private void ToogleMenu(object sender, RoutedEventArgs e)
        {
            if (isMenuExpanded)
            {
                isMenuExpanded = false;

                Storyboard anim = (Storyboard)Resources["InAnimation"];
                anim.Begin();

                Task.Run(
                    () =>
                        {
                            int leftThickness = 0;
                            while (leftThickness >= -300)
                            {
                                System.Threading.Thread.Sleep(10);
                                leftThickness -= 10;
                                Application.Current.Dispatcher.Invoke(() => { SideMenu.Margin = new Thickness(leftThickness, 0, 0, 0); });
                            }
                        });
            }
            else
            {
                isMenuExpanded = true;

                Storyboard anim = (Storyboard)Resources["OutAnimation"];
                anim.Begin();

                Task.Run(
                    () =>
                        {
                            int leftThickness = -300;
                            while (leftThickness != 0)
                            {
                                System.Threading.Thread.Sleep(13);
                                leftThickness += 10;
                                Application.Current.Dispatcher.Invoke(() => { SideMenu.Margin = new Thickness(leftThickness, 0, 0, 0); });
                            }
                        });
            }
        }

        #endregion UI Events
    }
}