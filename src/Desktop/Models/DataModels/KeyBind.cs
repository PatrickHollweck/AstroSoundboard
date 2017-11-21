// ****************************** Module Header ****************************** //
// 
// 
// Last Modified: 19:11:2017 / 18:46
// Creation: 19:11:2017
// Project: AstroSoundBoard
// 
// 
// <copyright file="KeyBind.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //



namespace AstroSoundBoard.Models.DataModels
{
    using System.Windows.Input;

    using Caliburn.Micro;

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class KeyBind : PropertyChangedBase
    {
        private ModifierKeys modifier;
        private Key key;

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

        public ModifierKeys Modifier
        {
            get => modifier;
            set
            {
                modifier = value;
                NotifyOfPropertyChange(nameof(Modifier));
            }
        }

        public Key Key
        {
            get => key;
            set
            {
                key = value;
                NotifyOfPropertyChange(nameof(Key));
            }
        }

        public bool HasAssignedKeybind => !(Key == Key.None && Modifier == ModifierKeys.None);

        // Needed for Json Serialisation.
        public string ModifierString => Modifier.ToString();

        public string KeyString => Key.ToString();
    }
}