// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:05:2017 / 17:38
// Creation: 18:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeyDetector.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AstroSoundBoard.Core.Utils
{
    public class KeyDetector
    {
        private static readonly byte[] DistinctVirtualKeys = Enumerable
            .Range(0, 256)
            .Select(KeyInterop.KeyFromVirtualKey)
            .Where(item => item != Key.None)
            .Distinct()
            .Select(item => (byte)KeyInterop.VirtualKeyFromKey(item))
            .ToArray();

        public List<Key> GetDownKeys()
        {
            var keyboardState = new byte[256];
            NativeMethods.GetKeyboardState(keyboardState);

            var downKeys = new List<Key>();
            for (var index = 0; index < DistinctVirtualKeys.Length; index++)
            {
                var virtualKey = DistinctVirtualKeys[index];
                if ((keyboardState[virtualKey] & 0x80) != 0)
                {
                    downKeys.Add(KeyInterop.KeyFromVirtualKey(virtualKey));
                }
            }

            return downKeys;
        }
    }
}