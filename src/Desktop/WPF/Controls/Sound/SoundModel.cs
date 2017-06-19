// ****************************** Module Header ****************************** //
//
//
// Last Modified: 15:06:2017 / 17:38
// Creation: 15:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Controls.Sound
{
    using AstroSoundBoard.Core.Objects.Models;

    using Newtonsoft.Json;

    using PropertyChanged;

    [ImplementPropertyChanged]
    public class SoundModel
    {
        public SoundModel(Sound def)
        {
            SoundDef = def;
            IconKind = SoundDef.IsFavorite == JsonConvert.True ? "Heart" : "HeartOutline";
        }

        public Sound SoundDef { get; set; }
        private string iconKind;

        public string IconKind
        {
            get => iconKind;
            set
            {
                iconKind = value;
                SoundDef.IsFavorite = iconKind == "HeardOutline" ? JsonConvert.False : JsonConvert.True;
            }
        }
    }
}