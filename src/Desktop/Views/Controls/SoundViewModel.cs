// ****************************** Module Header ****************************** //
// 
// 
// Last Modified: 18:11:2017 / 14:46
// Creation: 18:11:2017
// Project: AstroSoundBoard
// 
// 
// <copyright file="SoundViewModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //



namespace AstroSoundBoard.Controls.Sound
{
    using AstroSoundBoard.Objects.Interfaces;
    using AstroSoundBoard.Objects.Models;

    using Newtonsoft.Json;

    // TODO: Move viewmodel to viewmodels folder
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SoundViewModel : IAddableViewModel
    {
        public SoundModel Sound { get; set; }

        public SoundViewModel(SoundModel modelDef)
        {
            Sound = modelDef;
        }

        private string iconKind;
        public string IconKind
        {
            get => UpdateIcon();
            set => iconKind = value;
        }

        public string UpdateIcon()
        {
            return Sound.IsFavorite == JsonConvert.True ? "Heart" : "HeartOutline";
        }
    }
}