// ****************************** Module Header ****************************** //
//
//
// Last Modified: 12:05:2017 / 19:07
// Creation: 10:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="InfoWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Windows
{
    using System.Diagnostics;
    using System.IO;
    using System.Windows;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects.DataObjects;

    using Microsoft.Win32;

    using NAudio.Lame;
    using NAudio.Wave;

    public partial class InfoWindow : Window
    {
        public Sound LocalSound { get; set; }

        public InfoWindow(Sound def)
        {
            LocalSound = def;
            InitializeComponent();
            DataContext = this;
        }

        private void SaveSound(object sender, RoutedEventArgs e)
        {
            // Get the audio stream ( file from resources )
            LocalSound.Name = LocalSound.Name.Replace(" ", "_");
            var soundStream = (UnmanagedMemoryStream)SoundManager.GetAudioFileFromResources(LocalSound.Name);

            // Save it to the file if the getting was successful, yes getting, thats absolutely correct
            if (soundStream != null)
            {
                // Dialog to determine the file path
                var dialog = new SaveFileDialog
                {
                    Filter = ".mp3 File (*.mp3)|*.mp3",
                    Title = $"Sound Location for sound : {LocalSound.Name}"
                };
                dialog.ShowDialog();

                using (var reader = new WaveFileReader(soundStream))
                using (var writer = new LameMP3FileWriter(dialog.FileName, reader.WaveFormat, LAMEPreset.VBR_90))
                {
                    reader.CopyTo(writer);
                }

                MessageBox.Show($"Sound has been successfully created! At {dialog.FileName}", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("There was a problem creating the File.", "Error!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void OpenSoundsGit(object sender, RoutedEventArgs e) => Process.Start("https://github.com/FetzenRndy/AstroSoundboard/tree/master/src/AstroSoundBoard/Resources");
    }
}