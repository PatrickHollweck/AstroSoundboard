// ****************************** Module Header ****************************** //
// 
// 
// Last Modified: 11:05:2017 / 20:07
// Creation: 11:05:2017
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
    using AstroSoundBoard.Core.Objects.DataObjects;
    using AstroSoundBoard.WPF.Windows;

    using log4net;

    using Newtonsoft.Json;

    using PropertyChanged;

    [ImplementPropertyChanged]
    public partial class SoundView : UserControl
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Sound LocalDefinition { get; set; }

        public SoundView(Sound def)
        {
            Log.Debug($"Creating Control for {def.Name}");

            LocalDefinition = def;

            if (LocalDefinition.IsFavorite == JsonConvert.True)
            {
                IconKind = "Heart";
            }
            else
            {
                IconKind = "HeartOutline";
            }

            LocalDefinition.Name = LocalDefinition.Name.Replace('_', ' ');

            InitializeComponent();
            DataContext = this;
        }

        public string IconKind { get; set; }

        private void ToggleFavorite(object sender, RoutedEventArgs e)
        {
            if (LocalDefinition.IsFavorite == JsonConvert.True)
            {
                IconKind = "HeartOutline";
                LocalDefinition.IsFavorite = JsonConvert.False;

                SettingsManager.Update(LocalDefinition);
            }
            else
            {
                IconKind = "Heart";
                LocalDefinition.IsFavorite = JsonConvert.True;

                SettingsManager.Update(LocalDefinition);
            }
        }

        /// <summary>
        /// Shows informations for this sounds in a new window
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            var window = new InfoWindow(LocalDefinition);
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
                LocalDefinition.Name = LocalDefinition.Name.Replace(' ', '_');
                Log.Debug($"Trying to Play sound : {LocalDefinition.Name}");

                using (SoundPlayer player = new SoundPlayer((UnmanagedMemoryStream)SoundManager.GetAudioFileFromResources(LocalDefinition.Name)))
                {
                    player.Play();
                }

                LocalDefinition.Name = LocalDefinition.Name.Replace('_', ' ');
            }
            catch (Exception exception)
            {
                Log.Error("Can not play Definition!", exception);
            }
        }
    }
}