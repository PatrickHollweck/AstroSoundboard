// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 17:29
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindManagerWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AstroSoundBoard.Core.Components;
using AstroSoundBoard.WPF.Controls.Keybind;
using PropertyChanged;

namespace AstroSoundBoard.WPF.Windows
{
    [AddINotifyPropertyChangedInterface]
    public partial class KeybindManagerWindow : Window
    {
#pragma warning disable S1104 // Fields should not have public accessibility
        private static KeybindManagerWindow KeybindManagerInstance;
#pragma warning restore S1104 // Fields should not have public accessibility

        private readonly ItemManager<KeybindView> itemManager = new ItemManager<KeybindView>(model => new KeybindView(model) { Width = 550 });

        public KeybindManagerWindow()
        {
#pragma warning disable S3010 // Static fields should not be updated in constructors
            KeybindManagerInstance = this;
#pragma warning restore S3010 // Static fields should not be updated in constructors

            InitializeComponent();
            DataContext = this;
            itemManager.SetAll(ref ItemCtrl);
        }

        private void RemoveAllKeybinds(object sender, RoutedEventArgs e)
        {
            KeybindManager.RemoveAllKeybindsFromSettings();
            KeybindManager.SetKeybinds();
        }

        private void SearchForElement(object sender, TextChangedEventArgs e)
        {
            itemManager.Search(ref ItemCtrl, SearchBox.Text);
        }

        private void ToogleFavorites(object sender, RoutedEventArgs e)
        {
            itemManager.ToogleFavorites(ref ItemCtrl);
        }

        public static void Update()
        {
            KeybindManagerInstance?.itemManager.SetAll(ref KeybindManagerInstance.ItemCtrl);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
#pragma warning disable S2696 // Instance members should not write to "static" fields
            KeybindManagerInstance = null;
#pragma warning restore S2696 // Instance members should not write to "static" fields
            base.OnClosing(e);
        }
    }
}