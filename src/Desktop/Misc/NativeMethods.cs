// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 15:05
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="NativeMethods.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Misc
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// All native methods used in the Application (Fody forces this behavior!)
    /// </summary>
    public class NativeMethods
    {
        // Sound
        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        // Keybind
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetKeyboardState(byte[] keyState);
    }
}