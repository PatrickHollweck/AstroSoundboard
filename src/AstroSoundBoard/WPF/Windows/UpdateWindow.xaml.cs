// ****************************** Module Header ****************************** //
//
//
// Last Modified: 22:04:2017 / 22:36
// Creation: 22:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="UpdateWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Windows
{
    using System.Diagnostics;
    using System.Windows;

    /// <summary>
    /// Interaktionslogik für UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public UpdateWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/FetzenRndy/AstroSoundboard/blob/master/public/changelog.md");
        }
    }
}