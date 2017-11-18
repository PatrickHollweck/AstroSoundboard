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

namespace AstroSoundBoard.Views.Windows
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    using AstroSoundBoard.Services;
    using AstroSoundBoard.Views.Controls;

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class KeybindManagerWindow : Window
    {
        public static KeybindManagerWindow KeybindManagerInstance;

        private readonly ItemManager<KeybindView> itemManager = new ItemManager<KeybindView>(model => new KeybindView(model) { Width = 550 });

        public KeybindManagerWindow()
        {
            KeybindManagerInstance = this;

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
            KeybindManagerInstance = null;
            base.OnClosing(e);
        }
    }
}