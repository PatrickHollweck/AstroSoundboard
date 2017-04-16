// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:04:2017 / 22:17
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="MainWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Windows
{
	using System.Diagnostics;
	using System.Windows;

	using AstroSoundBoard.Core.Components;

	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			ViewChanger.MainWindowInstance = this;

			InitializeComponent();

			ViewChanger.ChangeViewTo(ViewChanger.Page.Board);
		}

		// UI Events
		private void ShowHome_Click(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo(ViewChanger.Page.Board);

		private void ShowSettings_Click(object sender, RoutedEventArgs e) => ViewChanger.ChangeViewTo(ViewChanger.Page.Settings);

		private void BrowserFaceBook_Click(object sender, RoutedEventArgs e) => Process.Start("https://www.facebook.com/TheAstronautKitty");

		private void BrowserTwitter_Click(object sender, RoutedEventArgs e) => Process.Start("https://twitter.com/AstroShitty");

		private void BrowserYoutube_Click(object sender, RoutedEventArgs e) => Process.Start("https://www.youtube.com/user/TheAstronautKitty");

		private void BrowserRequestFeature_Click(object sender, RoutedEventArgs e) => Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSc9JnTYAgQ2fbaSujj9-F3DsI6_BOXJGG7jsXNLD6Dqf11X9g/viewform?usp=sf_link");

		private void BrowserRequestSound_Click(object sender, RoutedEventArgs e) => Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSfVT7Jx-5n80LYFskIcjLsL3fb2RmI7XOXAS_2rtUFIuyYp8Q/viewform?usp=sf_link");

		private void BrowserReportIssue_Click(object sender, RoutedEventArgs e) => Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSdrhFCCVeKrbA56kiLpOqA5H1nszwA4gWimV1V6YOjXr5mC-A/viewform?usp=sf_link");

		private void BrowserGitHub_Click(object sender, RoutedEventArgs e) => Process.Start("https://github.com/FetzenRndy/AstroSoundboard");
	}
}