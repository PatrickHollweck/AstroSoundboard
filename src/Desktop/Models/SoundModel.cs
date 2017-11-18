// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 15:00
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Models
{
    using System;
    using System.IO;
    using System.Media;
    using System.Windows.Forms;
    using System.Windows.Input;

    using AstroSoundBoard.Misc.Extensions;
    using AstroSoundBoard.Services;

    using Caliburn.Micro;

    using Newtonsoft.Json;

    using ILog = log4net.ILog;
    using LogManager = log4net.LogManager;

    /// <summary>
    /// Model for a Sound.
    /// </summary>
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SoundModel : PropertyChangedBase
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string name;
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
                NotifyOfPropertyChange(nameof(Name));
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
                NotifyOfPropertyChange(nameof(isFavorite));
            }
        }

        /// <summary>
        /// Description of the Sound
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Link to the Video the Sound is originating from.
        /// </summary>
        public string VideoLink { get; private set; }

        /// <summary>
        /// The Hotkey.
        /// </summary>
        public KeyBind HotKey
        {
            get => hotKey ?? new KeyBind();
            set
            {
                hotKey = value;
                NotifyOfPropertyChange(nameof(HotKey));
            }
        }

        #region Methods

        public void PlaySound()
        {
            // TODO: In a future version I hope to implement mp3 files since currently I am using .wav which is lossless but quite big in size.
            try
            {
                Log.Debug($"Trying to Play sound : {Name}");

                var stream = (UnmanagedMemoryStream)SoundManager.GetAudioFileFromResources(FileName);

                if (stream == null)
                {
                    MessageBox.Show(@"Sorry this sound can not be played! Please contact the developers, with the name of the Sound you tried to play! (Stream was null)", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                using (var player = new SoundPlayer(stream))
                {
                    player.Play();
                }
            }
            catch (Exception exception)
            {
                Log.Error("Can not play Sound!", exception);
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

        /// <summary>
        /// Returns a SoundModel based on its <paramref name="jsonModel"/>
        /// </summary>
        /// <param name="jsonModel">Definition of the Sound</param>
        /// <returns>New SoundModel object</returns>
        public static SoundModel GetModel(JsonSoundModel jsonModel)
        {
            return new SoundModel
            {
                Name = jsonModel.Name,
                Description = jsonModel.Description,
                IsFavorite = jsonModel.IsFavorite,
                VideoLink = jsonModel.VideoLink,
                HotKey = new KeyBind
                {
                    Key = (Key)jsonModel.HotKey.Key,
                    Modifier = (ModifierKeys)jsonModel.HotKey.Modifier
                }
            };
        }

        #endregion Methods
    }
}