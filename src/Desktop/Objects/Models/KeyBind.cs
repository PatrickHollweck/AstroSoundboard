// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 17:12
// Creation: 20:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeyBind.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Objects.Models
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class KeyBind : INotifyPropertyChanged
    {
        public KeyBind(Key key, ModifierKeys modifier)
        {
            Modifier = modifier;
            Key = key;
        }

        public KeyBind()
        {
            Modifier = ModifierKeys.None;
            Key = Key.None;
        }

        private ModifierKeys modifier;
        private Key key;

        public ModifierKeys Modifier
        {
            get => modifier;
            set
            {
                modifier = value;
                OnPropertyChanged(nameof(Modifier));
            }
        }

        public Key Key
        {
            get => key;
            set
            {
                key = value;
                OnPropertyChanged(nameof(Key));
            }
        }

        public bool HasAssignedKeybind => !(Key == Key.None && Modifier == ModifierKeys.None);

        // Needed for Json Serialisation.
        public string ModifierString => Modifier.ToString();

        public string KeyString => Key.ToString();

        #region PropertyChanged

        public void RaisePropertyChanged()
        {
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion PropertyChanged
    }
}