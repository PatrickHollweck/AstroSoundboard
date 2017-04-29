// ****************************** Module Header ****************************** //
//
//
// Last Modified: 29:04:2017 / 20:11
// Creation: 29:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SafeNativeMethods.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Utils
{
    using System;
    using System.Runtime.InteropServices;

    public class NativeMethods
    {
        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
    }
}