// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 20:28
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="MainWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using AstroSoundBoard.Core.Components;
using AstroSoundBoard.Core.Utils;
using AstroSoundBoard.WPF.Pages.About;
using AstroSoundBoard.WPF.Pages.Board;
using AstroSoundBoard.WPF.Pages.Settings;
using AutoUpdaterDotNET;
using log4net;

namespace AstroSoundBoard.WPF.Windows
{
	public partial class MainWindow : Window
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public MainWindow()
		{
			AutoUpdater.Start("https://cdn.jsdelivr.net/gh/fetzenrndy/astrosoundboard@latest/public/versions/updaterInfo.xml");

			ViewChanger.MainWindowInstance = this;

			InitializeComponent();

			ViewChanger.ChangeViewTo<BoardView>();

			VolumeSlider_ValueChanged(new object(), new RoutedPropertyChangedEventArgs<double>(0, 50));
			VolumeSlider.Value = 50;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			KeybindManager.UnregisterAllKeybinds();
			SettingsManager.Save();
			base.OnClosing(e);
		}

		private bool CanSearchItemsExecute()
		{
			if (ViewChanger.MainWindowInstance.DataContext is BoardView)
			{
				SearchBox.IsEnabled = true;
				FavoriteButton.IsEnabled = true;
				return true;
			}

			SearchBox.IsEnabled = false;
			FavoriteButton.IsEnabled = false;
			return false;
		}

		public void SearchForItem(object sender, TextChangedEventArgs e)
		{
			if (!CanSearchItemsExecute())
			{
				return;
			}

			BoardView.BoardViewInstance?.SearchForElement(SearchBox.Text);
		}

		public void ToogleFavorites()
		{
			Log.Debug("Toggling Favorites!");

			if (!CanSearchItemsExecute())
			{
				return;
			}

			BoardView.BoardViewInstance?.OnlyShowFavorites();
		}

		private void FavoriteButton_Click(object sender, RoutedEventArgs e)
		{
			ToogleFavorites();
		}

		private void ContentControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			CanSearchItemsExecute();
		}

		private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			int newVolume = ushort.MaxValue / 100 * (int)e.NewValue;
			uint newVolumeAllChannels = ((uint)newVolume & 0x0000ffff) | ((uint)newVolume << 16);

			NativeMethods.waveOutSetVolume(IntPtr.Zero, newVolumeAllChannels);

			if (VolumeSlider.ToolTip != null && VolumeSlider.ToolTip is ToolTip toolTip)
			{
				toolTip.ToolTip = $"Volume: {newVolumeAllChannels}%";
				toolTip.IsOpen = true;
			}
		}

		private bool isMenuExpanded = true;

		private void ShowHome_Click(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo<BoardView>();

		private void ShowSettings_Click(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo<SettingsView>();

		private void BrowserFaceBook_Click(object sender, RoutedEventArgs e) => Process.Start(Properties.Resources.AstroSocial_Facebook);

		private void BrowserTwitter_Click(object sender, RoutedEventArgs e) => Process.Start(Properties.Resources.AstroSocial_Twitter);

		private void BrowserYoutube_Click(object sender, RoutedEventArgs e) => Process.Start(Properties.Resources.AstroSocial_Youtube);

		private void BrowserTwitch_Click(object sender, RoutedEventArgs e) => Process.Start(Properties.Resources.AstroSocial_Twitch);

		private void OpenFeedback(object sender, RoutedEventArgs e) => new FeedbackWindow().Show();

		private void SwitchToAbout(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo<AboutView>();

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
								Thread.Sleep(10);
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
								Thread.Sleep(13);
								leftThickness += 10;
								Application.Current.Dispatcher.Invoke(() => { SideMenu.Margin = new Thickness(leftThickness, 0, 0, 0); });
							}
						});
			}
		}
	}
}