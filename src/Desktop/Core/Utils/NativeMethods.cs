// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:05:2017 / 17:37
// Creation: 10:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="NativeMethods.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System;
using System.Runtime.InteropServices;

namespace AstroSoundBoard.Core.Utils
{
    /// <summary>
    /// All native methods used in the Application (Fody forces this behavior!)
    /// </summary>
    public static class NativeMethods
    {
        // SOUNDS
        [DllImport("winmm.dll")]
        internal static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        // Keybind
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetKeyboardState(byte[] keyState);
    }
}