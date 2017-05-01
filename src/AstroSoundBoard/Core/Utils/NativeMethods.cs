// ****************************** Module Header ****************************** //
//
//
// Last Modified: 30:04:2017 / 14:33
// Creation: 29:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="NativeMethods.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Utils
{
    using System;
    using System.Runtime.InteropServices;

    public class Win32
	{
		public const int WM_HOTKEY_MSG_ID = 0x0312;
	}

	public class NativeMethods
	{
		// SOUNDS
		[DllImport("winmm.dll")]
		public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
	}
}