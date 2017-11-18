// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 13:38
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Controls.Keybind
{
    using System.Windows;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects.Interfaces;
    using AstroSoundBoard.Core.Objects.Models;
    using AstroSoundBoard.Windows;

    using log4net;

    public partial class KeybindView : UserControl, IAddableView
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public KeybindViewModel Model { get; set; }
        public IAddableViewModel SoundModel
        {
            get => Model;
            set => Model = (KeybindViewModel)value;
        }

        public KeybindView(SoundModel soundModel)
        {
            Model = new KeybindViewModel(soundModel);

            InitializeComponent();
            DataContext = this;

            Log.Debug(Model.Sound.Name + " " + Model.Sound.HotKey.HasAssignedKeybind);

            Model.Sound.HotKey.PropertyChanged += (sender, args) => { CurrentKeybindPanel.Visibility = Model.Sound.HotKey.HasAssignedKeybind ? Visibility.Visible : Visibility.Hidden; };
            Model.Sound.HotKey.RaisePropertyChanged();
        }

        public void ConfigureKeybind(object sender, RoutedEventArgs e)
        {
            if (!KeybindConfiguratorWindow.HasOpenInstance)
            {
                new KeybindConfiguratorWindow(Model.Sound).Show();
            }
            else
            {
                MessageBox.Show("Only one instance of the Configuration Window allowed at a given Time... \nPlease close the other one.", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void RemoveKeybind(object sender, RoutedEventArgs e)
        {
            int index = SettingsManager.Cache.FindIndex(cacheSound => cacheSound.Name == Model.Sound.Name);
            SettingsManager.Cache[index].HotKey = new KeyBind();

            KeybindManager.SetKeybinds();
        }
    }
}