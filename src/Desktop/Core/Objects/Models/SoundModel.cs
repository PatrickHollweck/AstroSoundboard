// ****************************** Module Header ****************************** //
// 
// 
// Last Modified: 04:07:2017 / 20:08
// Creation: 20:06:2017
// Project: AstroSoundBoard
// 
// 
// <copyright file="SoundModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //



namespace AstroSoundBoard.Core.Objects.Models
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using AstroSoundBoard.Core.Objects.DataObjects.SoundDefinitionJsonTypes;
    using AstroSoundBoard.Core.Utils.Extensions;

    using Newtonsoft.Json;

    /// <summary>
    /// Model for a Sound.
    /// </summary>
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SoundModel : INotifyPropertyChanged
    {
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