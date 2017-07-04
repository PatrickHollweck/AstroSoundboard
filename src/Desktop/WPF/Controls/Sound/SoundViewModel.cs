// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 16:46
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundViewModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Controls.Sound
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

        public string IconKind => Sound.IsFavorite == JsonConvert.True ? "Heart" : "HeartOutline";
    }
}