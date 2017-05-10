// ****************************** Module Header ****************************** //
//
//
// Last Modified: 22:04:2017 / 16:16
// Creation: 22:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="InfoWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Windows
{
    using System.ComponentModel;
    using System.Windows;

    using AstroSoundBoard.Core.Objects.DataObjects;

    public partial class InfoWindow : Window
    {
        public Sound LocalSound { get; set; }

        public InfoWindow(Sound def)
        {
            LocalSound = def;
            InitializeComponent();
            DataContext = this;

        }
    }
}