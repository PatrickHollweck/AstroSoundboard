// ****************************** Module Header ****************************** //
//
//
// Last Modified: 08:05:2017 / 17:57
// Creation: 08:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="Sound.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Objects.DataObjects
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// A sound. (Implements INotifyPropertyChanged!)
    /// </summary>
    public class Sound : INotifyPropertyChanged
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

        /// <summary>
        /// String Indicating if the Sound is a favorite.
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
            get => hotKey;
            set
            {
                hotKey = value;
                OnPropertyChanged(nameof(HotKey));
            }
        }

        /// <summary>
        /// Helper to Raise the Property changed event;
        /// </summary>
        public void RaisePropertyChanged()
        {
            OnPropertyChanged();
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion PropertyChanged
    }
}