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

using AstroSoundBoard.Core.Objects.Models;
using AstroSoundBoard.Core.Objects.Interfaces;

using Newtonsoft.Json;
using PropertyChanged;

namespace AstroSoundBoard.WPF.Controls.Sound
{
    [AddINotifyPropertyChangedInterface]
    public class SoundViewModel : IAddableViewModel
    {
        public SoundModel Sound { get; set; }
        public string IconKind { get;set; }

        public SoundViewModel(SoundModel modelDef)
        {
            Sound = modelDef;
        }

        public void UpdateIcon()
        {
            IconKind = Sound.IsFavorite == JsonConvert.True ? "Heart" : "HeartOutline";
        }
    }
}