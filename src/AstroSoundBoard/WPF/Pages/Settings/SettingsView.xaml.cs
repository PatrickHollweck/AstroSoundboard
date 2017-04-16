// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:04:2017 / 22:23
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SettingsView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Pages.Settings
{
	using System.Windows.Controls;

	public partial class SettingsView : UserControl
	{
		public string CurrentVersion { get; } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();

		public SettingsView()
		{
			InitializeComponent();
			DataContext = this;
		}
	}
}