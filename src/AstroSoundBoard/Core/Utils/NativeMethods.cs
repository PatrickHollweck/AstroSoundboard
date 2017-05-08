// ****************************** Module Header ****************************** //
//
//
// Last Modified: 08:05:2017 / 18:00
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

    /// <summary>
    /// All native methods used in the Application (Fody forces this behavior!)
    /// </summary>
    public class NativeMethods
    {
        // SOUNDS
        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
    }
}