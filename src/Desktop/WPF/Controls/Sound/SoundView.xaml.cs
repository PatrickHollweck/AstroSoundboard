// ****************************** Module Header ****************************** //
// 
// 
// Last Modified: 04:07:2017 / 18:33
// Creation: 01:07:2017
// Project: AstroSoundBoard
// 
// 
// <copyright file="SoundView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //



namespace AstroSoundBoard.WPF.Controls.Sound
{
    using System;
    using System.IO;
    using System.Media;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects.Interfaces;
    using AstroSoundBoard.Core.Objects.Models;
    using AstroSoundBoard.Core.Utils.Extensions;
    using AstroSoundBoard.WPF.Windows;

    using log4net;

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class SoundView : UserControl, IAddableView
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public SoundViewModel Model { get; set; }

        public IAddableViewModel SoundModel
        {
            get => Model;
            set => Model = (SoundViewModel)value;
        }

        public SoundView(SoundModel def)
        {
            Log.Debug($"Creating Control for {def.Name}");

            Model = new SoundViewModel(def);
            Model.Sound.Name = Model.Sound.Name.ToDisplayName();

            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Toggles the Favorite Setting
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private void ToggleFavorite(object sender, RoutedEventArgs e) => SettingsManager.Update(Model.Sound);

        /// <summary>
        /// Opens a new Window with Informations about the Sound
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            var window = new InfoWindow(Model.Sound);
            window.Show();
        }

        /// <summary>
        /// Plays the Sound
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void PlaySound(object sender, RoutedEventArgs e)
        {
            // In a future version I hope to implement mp3 files since currently I am using .wave which is lossless and quite big in size.
            try
            {
                Model.Sound.Name = Model.Sound.Name.ToFileName();

                Log.Debug($"Trying to Play sound : {Model.Sound.Name}");

                using (SoundPlayer player = new SoundPlayer((UnmanagedMemoryStream)SoundManager.GetAudioFileFromResources(Model.Sound.Name)))
                {
                    player.Play();
                }

                Model.Sound.Name = Model.Sound.Name.ToDisplayName();
            }
            catch (Exception exception)
            {
                Log.Error("Can not play Definition!", exception);
            }
        }
    }
}