// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:07:2017 / 19:37
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundViewModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Controls.Sound
{
    using AstroSoundBoard.Core.Objects.Interfaces;
    using AstroSoundBoard.Core.Objects.Models;

    using Newtonsoft.Json;

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