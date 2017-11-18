// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 14:45
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindViewModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Controls.Keybind
{
    using AstroSoundBoard.Objects.Interfaces;
    using AstroSoundBoard.Objects.Models;

    // TODO: Move viewmodel to viewmodels folder!
    public class KeybindViewModel : IAddableViewModel
    {
        public SoundModel Sound { get; set; }

        public KeybindViewModel(SoundModel modelDef)
        {
            Sound = modelDef;
        }
    }
}