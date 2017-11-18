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

namespace AstroSoundBoard.Core.Objects.Interfaces
{
    using AstroSoundBoard.Core.Objects.Models;

    public interface IAddableViewModel
    {
        SoundModel Sound { get; }
    }
}