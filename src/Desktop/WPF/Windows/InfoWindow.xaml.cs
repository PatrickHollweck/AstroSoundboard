// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 20:59
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="InfoWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System.IO;
using System.Diagnostics;
using System.Windows;

using AstroSoundBoard.Core.Components;
using AstroSoundBoard.Core.Objects;
using AstroSoundBoard.Core.Objects.Models;
using AstroSoundBoard.Core.Utils.Extensions;

using Microsoft.Win32;

using NAudio.Wave;

namespace AstroSoundBoard.WPF.Windows
{
    public partial class InfoWindow : Window
    {
        public SoundModel Model { get; set; }

        public InfoWindow(SoundModel def)
        {
            Model = def;
            InitializeComponent();
            DataContext = this;
        }

        // TODO: This needs more abstractions - But until I refactor the app structure this is better than before.
        private void SaveSound(object sender, RoutedEventArgs e)
        {
            // Get the audio stream ( file from resources )
            var soundStream = (UnmanagedMemoryStream)SoundManager.GetAudioFileFromResources(Model.Name.ToFileName());

            if (soundStream == null)
            {
                MessageBox.Show("There was a internal problem - Could not get sound source stream!", "Error", MessageBoxButton.OK);
                return;
            }

            var dialog = new SaveFileDialog
            {
                InitialDirectory = $"{AppSettings.AssemblyDirectory}",
                Filter = "Wave sound file (*.wav)|*.wav",
                Title = $"Sound Location for sound : {Model.Name}",
                FileName = Model.Name
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            // Fill the input stream with the sound data.
            var inputStream = new MemoryStream();
            var buffer = new byte[65536];
            var bytesRead = soundStream.Read(buffer, 0, buffer.Length);

            while (bytesRead > 0)
            {
                inputStream.Write(buffer, 0, bytesRead);
                bytesRead = soundStream.Read(buffer, 0, buffer.Length);
            }

            // Reset the input streams position to the start
            inputStream.Position = 0;

            // Start writing the file to the disk
            using (var mp3Stream = new WaveFileReader(inputStream))
            {    
                WaveFileWriter.CreateWaveFile(dialog.FileName, mp3Stream);
            }

            soundStream.Dispose();

            MessageBox.Show($"Sound has been successfully created! At {dialog.FileName}", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OpenSoundsGit(object sender, RoutedEventArgs e) => Process.Start($"{Properties.Resources.Project_Github}/tree/master/src/AstroSoundBoard/Resources");
    }
}