// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:05:2017 / 19:32
// Creation: 10:05:2017
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
    using AstroSoundBoard.Core.Objects.Models;
    using AstroSoundBoard.WPF.Windows;

    public partial class KeybindView : UserControl
    {
        /// <summary>
        /// Local Instance of the Sound
        /// </summary>
        public Sound LocalDefinition { get; set; }

        public KeybindView(Sound sound)
        {
            LocalDefinition = SettingsManager.GetSound(sound.Name);

            InitializeComponent();
            DataContext = this;

            // When migrating from older versions the Hotkey object may be null -> set it to a new Keybind
            if (LocalDefinition.HotKey == null)
            {
                LocalDefinition.HotKey = new KeyBind();
            }

            // The Visibility of the "What keybind is set for this sound" should be hidden by default if there is no Hotkey.
            LocalDefinition.HotKey.PropertyChanged += (sender, args) => { CurrentKeybindPanel.Visibility = LocalDefinition.HotKey.HasAssignedKeybind ? Visibility.Hidden : Visibility.Visible; };
            LocalDefinition.HotKey.RaisePropertyChanged();
        }

        public void ConfigureKeybind(object sender, RoutedEventArgs e)
        {
            if (!KeybindConfiguratorWindow.HasOpenInstance)
            {
                new KeybindConfiguratorWindow(LocalDefinition).Show();
            }
            else
            {
                MessageBox.Show("Only one instance of the Configuration Window allowed at a Time... \nPlease close the other one.", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void RemoveKeybind(object sender, RoutedEventArgs e)
        {
            int index = SettingsManager.Cache.FindIndex(cacheSound => cacheSound.Name == LocalDefinition.Name);
            SettingsManager.Cache[index].HotKey = new KeyBind();
            SettingsManager.Cache[index].HotKey.RaisePropertyChanged();

            KeybindManager.SetKeybinds();

            LocalDefinition.HotKey.RaisePropertyChanged();
        }
    }
}