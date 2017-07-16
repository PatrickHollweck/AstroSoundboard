// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:07:2017 / 19:12
// Creation: 20:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Objects.Models
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Media;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects.DataObjects.SoundDefinitionJsonTypes;
    using AstroSoundBoard.Core.Utils.Extensions;

    using log4net;

    using Newtonsoft.Json;

    /// <summary>
    /// Model for a Sound.
    /// </summary>
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SoundModel : INotifyPropertyChanged
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string name;
        private string videoLink;
        private string isFavorite;
        private KeyBind hotKey;

        /// <summary>
        /// Name of the Sound.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string DisplayName => Name.ToDisplayName();
        public string FileName => Name.ToFileName();

        /// <summary>
        /// String Indicating if the Sound is a favorite. ( Compare with JsonConvert.True )
        /// </summary>
        public string IsFavorite
        {
            // This has to be a string and not a bool so it can be serialized.
            get => isFavorite;
            set
            {
                isFavorite = value;
                OnPropertyChanged(nameof(isFavorite));
            }
        }

        /// <summary>
        /// Description of the Sound
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Link to the Video the Sound is originating from.
        /// </summary>
        public string VideoLink
        {
            get => videoLink;
            set
            {
                videoLink = value;
                OnPropertyChanged(nameof(VideoLink));
            }
        }

        /// <summary>
        /// The Hotkey.
        /// </summary>
        public KeyBind HotKey
        {
            get => hotKey ?? new KeyBind();
            set
            {
                hotKey = value;
                OnPropertyChanged(nameof(HotKey));
            }
        }

        #region PropertyChanged

        /// <summary>
        /// Helper to Raise the Property changed event;
        /// </summary>
        public void RaisePropertyChanged()
        {
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion PropertyChanged

        #region Methods

        public void PlaySound()
        {
            // TODO: In a future version I hope to implement mp3 files since currently I am using .wave which is lossless and quite big in size.
            try
            {
                Log.Debug($"Trying to Play sound : {Name}");

                var stream = (UnmanagedMemoryStream)SoundManager.GetAudioFileFromResources(FileName);

                if (stream == null)
                {
                    MessageBox.Show(@"Sorry this sound can not be played! Please contact the developers, with the name of the Sound you tried to play!", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                using (SoundPlayer player = new SoundPlayer(stream))
                {
                    player.Play();
                }
            }
            catch (Exception exception)
            {
                Log.Error("Can not play Definition!", exception);
            }
        }

        /// <summary>
        /// Returns a SoundModel based on its <paramref name="definition"/>
        /// </summary>
        /// <param name="definition">Definition of the Sound</param>
        /// <returns>New SoundModel object</returns>
        public static SoundModel GetModel(Definition definition)
        {
            return new SoundModel
            {
                Name = definition.Sound.Name,
                Description = definition.Info.Description,
                IsFavorite = JsonConvert.False,
                VideoLink = definition.Info.VideoLink,
                HotKey = new KeyBind()
            };
        }

        #endregion Methods
    }
}