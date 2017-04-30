// ****************************** Module Header ****************************** //
//
//
// Last Modified: 01:05:2017 / 00:52
// Creation: 29:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Controls.Keybind
{
    using System.Windows;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects.DataObjects;
    using AstroSoundBoard.WPF.Windows;

    public partial class KeybindView : UserControl
    {
        public Sound LocalDefinition { get; set; }

        public KeybindView(Sound sound)
        {
            LocalDefinition = SettingsManager.GetSound(sound.Name);

            InitializeComponent();
            DataContext = this;

            LocalDefinition.HotKey.PropertyChanged += (sender, args) =>
                {
                    if (LocalDefinition.HotKey.HasAssignedKeybind)
                    {
                        CurrentKeybindPanel.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        CurrentKeybindPanel.Visibility = Visibility.Visible;
                    }
                };

            LocalDefinition.HotKey.RaisePropertyChanged();
        }

        public void ConfigureKeybind(object sender, RoutedEventArgs e) => new KeybindConfiguratorWindow(LocalDefinition).Show();

        public void RemoveKeybind(object sender, RoutedEventArgs e)
        {
            KeybindManager.RemoveKeybindAndMappingByName(LocalDefinition.Name);
            LocalDefinition.HotKey.RaisePropertyChanged();
        }
    }
}