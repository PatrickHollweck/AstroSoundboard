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

using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using AstroSoundBoard.Core.Components;
using AstroSoundBoard.Core.Objects;
using AstroSoundBoard.Core.Objects.Models;
using AstroSoundBoard.Core.Utils.Extensions;
using Microsoft.Win32;
using NAudio.Lame;
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

        private async void SaveSound(object sender, RoutedEventArgs e)
        {
            // Get the audio stream ( file from resources )
            var soundStream = (UnmanagedMemoryStream)SoundManager.GetAudioFileFromResources(Model.Name.ToFileName());

            if (!File.Exists($"{AppSettings.AssemblyDirectory}/libmp3lame.32.dll"))
            {
                var result = MessageBox.Show("A library is required to be able to save Sounds. If you click OK the library will be downloaded automatically, and you will be able to save the Sound.", "Notification", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);

                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        using (WebClient wc = new WebClient())
                        {
                            await wc.DownloadFileTaskAsync(@"http://www.github.com/FetzenRndy/AstroSoundboard/raw/develop/src/Desktop/libmp3lame.32.dll", "libmp3lame.32.dll");
                        }

                        MessageBox.Show("Download Completed! \nNow there is a file named libmp3lame.32.dll in the directory, you started the Programm from, when you are done saving sounds, you can delete this file savely again! \nNow you can Save Sounds :) Have fun.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    catch
                    {
                        MessageBox.Show("Error, while downloading!");
                        return;
                    }
                }
            }

            // Save it to the file if the getting was successful, yes getting, thats absolutely correct
            if (soundStream != null)
            {
                // Dialog to determine the file path
                var dialog = new SaveFileDialog
                {
                    InitialDirectory = $"{AppSettings.AssemblyDirectory}",
                    Filter = ".mp3 File (*.mp3)|*.mp3",
                    Title = $"Sound Location for sound : {Model.Name}",
                    FileName = Model.Name
                };

                if (dialog.ShowDialog() != true)
                {
                    return;
                }

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

        private void OpenSoundsGit(object sender, RoutedEventArgs e) => Process.Start($"{Properties.Resources.Project_Github}/tree/master/src/AstroSoundBoard/Resources");
    }
}