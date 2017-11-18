// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 14:44
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="AboutView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Views.Pages
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void OpenGitHub(object sender, RoutedEventArgs e) => Process.Start("https://github.com/FetzenRndy/AstroSoundboard");

        private void ShowDiscordInfo(object sender, RoutedEventArgs e) => MessageBox.Show("Add me on discord : Fetzen.NET#9296");

        private void OpenMail(object sender, RoutedEventArgs e) => Process.Start("mailto:patrick-hollweck@gmx.de");
    }
}