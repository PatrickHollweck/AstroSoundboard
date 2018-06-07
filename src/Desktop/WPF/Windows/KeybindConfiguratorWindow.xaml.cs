// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 17:30
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindConfiguratorWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AstroSoundBoard.Core.Components;
using AstroSoundBoard.Core.Objects.Models;
using AstroSoundBoard.Core.Utils;
using PropertyChanged;

namespace AstroSoundBoard.WPF.Windows
{
    [AddINotifyPropertyChangedInterface]
    public partial class KeybindConfiguratorWindow : Window
    {
        public static bool HasOpenInstance { get; set; }
        public SoundModel Model { get; set; }

        public KeybindConfiguratorWindow(SoundModel definition)
        {
            InitializeComponent();
            DataContext = this;

            HasOpenInstance = true;

            Model = SettingsManager.GetSound(definition.Name);

            if (Model.HotKey == null)
            {
                Model.HotKey = new KeyBind();
            }
        }

        private void ResetKeybind(object sender, RoutedEventArgs e)
        {
            Model.HotKey.Key = Key.None;
            Model.HotKey.Modifier = ModifierKeys.None;
            UpdateText();
        }

        private void ApplyKeybind(object sender, RoutedEventArgs e)
        {
            int index = SettingsManager.Cache.FindIndex(cacheSound => cacheSound.Name == Model.Name);
            SettingsManager.Cache[index] = Model;
            SettingsManager.Save();

            KeybindManager.SetKeybinds();

            KeybindManagerWindow.Update();
            Close();
        }

        private void UpdateText() => KeybindBox.Text = $"{Model.HotKey.Modifier} + {Model.HotKey.Key}";

        protected override void OnKeyDown(KeyEventArgs e)
        {
            foreach (Key key in new KeyDetector().GetDownKeys())
            {
                // Check if key is modifier.
                if (key == Key.LeftCtrl || key == Key.RightCtrl)
                {
                    Model.HotKey.Modifier = ModifierKeys.Control;
                }
                else if (key == Key.LeftAlt || key == Key.RightAlt)
                {
                    Model.HotKey.Modifier = ModifierKeys.Alt;
                }
                else if (key == Key.LeftShift || key == Key.RightShift)
                {
                    Model.HotKey.Modifier = ModifierKeys.Shift;
                }
                else if (key == Key.LWin || key == Key.RWin)
                {
                    Model.HotKey.Modifier = ModifierKeys.Windows;
                }
                else
                {
                    // Key is not a modifier -> Is key
                    Model.HotKey.Key = key;
                }

                if (KeybindManager.CheckDuplicate(Model))
                {
                    Model.HotKey.Key = Key.None;
                    Model.HotKey.Modifier = ModifierKeys.None;
                    MessageBox.Show("This Keybind is already defined somewhere else! Please choose another one!");
                }

                UpdateText();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            HasOpenInstance = false;
            base.OnClosing(e);
        }
    }
}