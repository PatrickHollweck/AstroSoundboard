// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 16:32
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="IAddableView.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Objects.Interfaces
{
    public interface IAddableView
    {
        IAddableViewModel SoundModel { get; set; }
    }
}