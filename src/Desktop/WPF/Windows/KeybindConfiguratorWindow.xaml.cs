// ****************************** Module Header ****************************** //
// 
// 
// Last Modified: 01:07:2017 / 11:22
// Creation: 01:07:2017
// Project: AstroSoundBoard
// 
// 
// <copyright file="KeybindConfiguratorWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //



namespace AstroSoundBoard.WPF.Windows
{
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects.Models;
    using AstroSoundBoard.Core.Utils;

    using log4net;

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class KeybindConfiguratorWindow : Window
    {
        public static bool HasOpenInstance { get; set; }

        public Sound LocalDefinition { get; set; }

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public KeybindConfiguratorWindow(Sound definition)
        {
            InitializeComponent();
            DataContext = this;

            HasOpenInstance = true;

            LocalDefinition = SettingsManager.GetSound(definition.Name);

            if (LocalDefinition.HotKey == null)
            {
                LocalDefinition.HotKey = new KeyBind();
            }
        }

        private void ResetKeybind(object sender, RoutedEventArgs e)
        {
            LocalDefinition.HotKey.Key = Key.None;
            LocalDefinition.HotKey.Modifier = ModifierKeys.None;
            UpdateText();
        }

        private void ApplyKeybind(object sender, RoutedEventArgs e)
        {
            int index = SettingsManager.Cache.FindIndex(cacheSound => cacheSound.Name == LocalDefinition.Name);
            SettingsManager.Cache[index] = LocalDefinition;

            KeybindManager.SetKeybinds();
        }

        private void UpdateText() => KeybindBox.Text = $"{LocalDefinition.HotKey.Modifier} + {LocalDefinition.HotKey.Key}";

        protected override void OnKeyDown(KeyEventArgs e)
        {
            foreach (Key key in new KeyDetector().GetDownKeys())
            {
                // Check if key is modifier.
                if (key == Key.LeftCtrl || key == Key.RightCtrl)
                {
                    LocalDefinition.HotKey.Modifier = ModifierKeys.Control;
                }
                else if (key == Key.LeftAlt || key == Key.RightAlt)
                {
                    LocalDefinition.HotKey.Modifier = ModifierKeys.Alt;
                }
                else if (key == Key.LeftShift || key == Key.RightShift)
                {
                    LocalDefinition.HotKey.Modifier = ModifierKeys.Shift;
                }
                else if (key == Key.LWin || key == Key.RWin)
                {
                    LocalDefinition.HotKey.Modifier = ModifierKeys.Windows;
                }
                else
                {
                    // Key is not a modifier -> Is key
                    LocalDefinition.HotKey.Key = key;
                }

                if (KeybindManager.CheckDuplicate(LocalDefinition))
                {
                    LocalDefinition.HotKey.Key = Key.None;
                    LocalDefinition.HotKey.Modifier = ModifierKeys.None;
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