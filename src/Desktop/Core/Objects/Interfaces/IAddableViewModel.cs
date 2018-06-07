// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 16:31
// Creation: 04:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="IAddableViewModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using AstroSoundBoard.Core.Objects.Models;

namespace AstroSoundBoard.Core.Objects.Interfaces
{
    public interface IAddableViewModel
    {
        SoundModel Sound { get; }
    }
}