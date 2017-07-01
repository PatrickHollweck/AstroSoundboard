// ****************************** Module Header ****************************** //
// 
// 
// Last Modified: 01:07:2017 / 11:22
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
    using AstroSoundBoard.Core.Extensions;
    using AstroSoundBoard.Core.Objects.Models;
    using AstroSoundBoard.WPF.Windows;

    using log4net;

    using Newtonsoft.Json;

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class SoundView : UserControl
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public SoundModel Model { get; set; }

        public SoundView(Sound def)
        {
            Log.Debug($"Creating Control for {def.Name}");

            Model = new SoundModel(def);
            Model.SoundDef.Name = Model.SoundDef.Name.ToDisplayName();

            InitializeComponent();
            DataContext = this;
        }

        private void ToggleFavorite(object sender, RoutedEventArgs e)
        {
            Model.SoundDef.IsFavorite = Model.SoundDef.IsFavorite == JsonConvert.True ? JsonConvert.False : JsonConvert.True;
            Model.IconKind = Model.SoundDef.IsFavorite == JsonConvert.True ? "HeartOutline" : "Heart";
            SettingsManager.Update(Model.SoundDef);
        }

        /// <summary>
        /// Shows informations for this sounds in a new window
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            var window = new InfoWindow(Model.SoundDef);
            window.Show();
        }

        /// <summary>
        /// Plays the Sound
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void PlaySound(object sender, RoutedEventArgs e)
        {
            // In a future version I hope to implement mp3 files since currently I am using .wave which is lossless and quite big.
            try
            {
                Model.SoundDef.Name = Model.SoundDef.Name.ToFileName();

                Log.Debug($"Trying to Play sound : {Model.SoundDef.Name}");

                using (SoundPlayer player = new SoundPlayer((UnmanagedMemoryStream)SoundManager.GetAudioFileFromResources(Model.SoundDef.Name)))
                {
                    player.Play();
                }

                Model.SoundDef.Name = Model.SoundDef.Name.ToDisplayName();
            }
            catch (Exception exception)
            {
                Log.Error("Can not play Definition!", exception);
            }
        }
    }
}