// ****************************** Module Header ****************************** //
//
//
// Last Modified: 01:05:2017 / 13:46
// Creation: 01:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindConfiguratorWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Windows
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects.DataObjects;

    using log4net;

    using PropertyChanged;

    [ImplementPropertyChanged]
    public partial class KeybindConfiguratorWindow : Window
    {
        public static bool HasOpenInstance { get; set; }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Sound LocalDefinition { get; set; }

        private Key PressedKey { get; set; }

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

            LoadState();

            KeyDown += (sender, e) =>
                {
                    PressedKey = e.Key;
                    BoundKey.Text = e.Key.ToString();
                    LocalDefinition.HotKey.Key = e.Key;
                    CheckDuplicate();
                };
        }

        private void LoadState()
        {
            Log.Debug("LOADING STATE!");
            LocalDefinition.HotKey.SetValues();

            if (LocalDefinition.HotKey.Modifier == ModifierKeys.Alt)
            {
                AltModifier.IsChecked = true;
            }

            if (LocalDefinition.HotKey.Modifier == ModifierKeys.Control)
            {
                CtrlModifier.IsChecked = true;
            }

            if (LocalDefinition.HotKey.Modifier == ModifierKeys.Windows)
            {
                WindowsModifier.IsChecked = true;
            }

            if (LocalDefinition.HotKey.Modifier == ModifierKeys.Shift)
            {
                ShiftModifier.IsChecked = true;
            }

            BoundKey.Text = LocalDefinition.HotKey.Key.ToString();
        }

        private void ApplyKeybind(object sender, RoutedEventArgs e)
        {
            SettingsManager.RewriteSound(LocalDefinition);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            HasOpenInstance = false;
            KeybindManager.SetKeybinds();
            base.OnClosing(e);
        }

        private void ModifierChanged(object sender, RoutedEventArgs e)
        {
            bool control = CtrlModifier.IsChecked != null && (bool)CtrlModifier.IsChecked;
            bool shift = ShiftModifier.IsChecked != null && (bool)ShiftModifier.IsChecked;
            bool windows = WindowsModifier.IsChecked != null && (bool)WindowsModifier.IsChecked;
            bool alternative = AltModifier.IsChecked != null && (bool)AltModifier.IsChecked;

            int checkedCount = 0;

            if (windows)
            {
                checkedCount++;
            }

            if (shift)
            {
                checkedCount++;
            }

            if (control)
            {
                checkedCount++;
            }

            if (alternative)
            {
                checkedCount++;
            }

            if (checkedCount > 1)
            {
                MessageBox.Show("Only one Modifier allowed... Support for more comming in a future version!");

                CtrlModifier.IsChecked = false;
                ShiftModifier.IsChecked = false;
                WindowsModifier.IsChecked = false;
                AltModifier.IsChecked = false;
            }
            else
            {
                if (windows)
                {
                    LocalDefinition.HotKey.Modifier = ModifierKeys.Windows;
                    return;
                }

                if (shift)
                {
                    LocalDefinition.HotKey.Modifier = ModifierKeys.Shift;
                    return;
                }

                if (control)
                {
                    LocalDefinition.HotKey.Modifier = ModifierKeys.Control;
                    return;
                }

                if (alternative)
                {
                    LocalDefinition.HotKey.Modifier = ModifierKeys.Alt;
                    return;
                }

                LocalDefinition.HotKey.Modifier = ModifierKeys.None;
            }

            CheckDuplicate();
        }

        private void CheckDuplicate()
        {
            (bool status, string message) checkResult = KeybindManager.CheckDuplicate(LocalDefinition);

            if (!checkResult.status)
            {
                MessageBox.Show(checkResult.message, "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                ApplyButton.IsEnabled = false;
            }
            else
            {
                ApplyButton.IsEnabled = true;
            }
        }
    }
}