// ****************************** Module Header ****************************** //
//
//
// Last Modified: 01:05:2017 / 01:11
// Creation: 21:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="Sound.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Objects.DataObjects
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class Sound : INotifyPropertyChanged
    {
        private string name;
        private string videoLink;
        private string isFavorite;
        private KeyBind hotKey;

        public string Name
        {
            get => name;

            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string IsFavorite
        {
            get => isFavorite;
            set
            {
                isFavorite = value;
                OnPropertyChanged(nameof(isFavorite));
            }
        }

        public string Description { get; set; }

        public string VideoLink
        {
            get => videoLink;
            set
            {
                videoLink = value;
                OnPropertyChanged(nameof(VideoLink));
            }
        }

        public KeyBind HotKey
        {
            get => hotKey;
            set
            {
                hotKey = value;
                OnPropertyChanged(nameof(HotKey));
            }
        }

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