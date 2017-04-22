// ****************************** Module Header ****************************** //
//
//
// Last Modified: 22:04:2017 / 15:58
// Creation: 17:04:2017
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
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Sound LocalDefinition { get; set; }

        public SoundView(Sound def)
        {
            // This need's to be done - we want a clone not a reference
            LocalDefinition = def;

            if (LocalDefinition.Name.Contains("_"))
            {
                LocalDefinition.Name = LocalDefinition.Name.Replace('_', ' ');
            }

            if (LocalDefinition.IsFavorite == JsonConvert.True)
            {
                IconKind = "Heart";
            }
            else
            {
                IconKind = "HeartOutline";
            }

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

                SettingsManager.RewriteSound(LocalDefinition);
            }
            else
            {
                IconKind = "Heart";

                LocalDefinition.IsFavorite = JsonConvert.True;

                SettingsManager.RewriteSound(LocalDefinition);
            }
        }

        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            var window = new InfoWindow(LocalDefinition);
            window.Show();
        }

        private void PlaySound(object sender, RoutedEventArgs e)
        {
            try
            {
                LocalDefinition.Name = LocalDefinition.Name.Replace(' ', '_');
                Log.Debug($"Trying to Play sound : {LocalDefinition.Name}");

                using (SoundPlayer player = new SoundPlayer((UnmanagedMemoryStream)SoundManager.Storage.GetAudioFileFromResources(LocalDefinition.Name)))
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