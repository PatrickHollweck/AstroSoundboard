// ****************************** Module Header ****************************** //
//
//
// Last Modified: 08:05:2017 / 14:35
// Creation: 08:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="NativeMethods.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Utils
{
    using System;
    using System.Runtime.InteropServices;

    public class NativeMethods
    {
        // SOUNDS
        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
    }
}