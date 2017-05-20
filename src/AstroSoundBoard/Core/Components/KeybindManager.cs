// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:05:2017 / 19:30
// Creation: 12:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindManager.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;
    using System.IO;
    using System.Media;
    using System.Reflection;

    using AstroSoundBoard.Core.Objects.DataObjects;

    using log4net;

    using NHotkey;
    using NHotkey.Wpf;

    public class KeybindManager
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void SetKeybinds()
        {
            foreach (Sound sound in SettingsManager.Cache)
            {
                HotkeyManager.Current.Remove(sound.Name);
            }

            foreach (Sound sound in SettingsManager.Cache)
            {
                if (sound.HotKey == null)
                {
                    sound.HotKey = new KeyBind();
                }

                try
                {
                    HotkeyManager.Current.AddOrReplace(sound.Name, sound.HotKey.Key, sound.HotKey.Modifier, PlaySound);
                }
                catch (HotkeyAlreadyRegisteredException)
                {
                    // This exception will occur often but is not a problem, since then there is already a hotkey registered for that key - name, which is fine.
                }
            }
        }

        public static bool CheckDuplicate(Sound sound)
        {
            // Poor mans implementation.
            try
            {
                HotkeyManager.Current.AddOrReplace(sound.Name, sound.HotKey.Key, sound.HotKey.Modifier, PlaySound);
            }
            catch (HotkeyAlreadyRegisteredException)
            {
                return true;
            }

            return false;
        }

        public static void UnregisterAllKeybinds()
        {
            foreach (Sound sound in SettingsManager.Cache)
            {
                HotkeyManager.Current.Remove(sound.Name);
            }
        }

        public static void RemoveAllKeybindsFromSettings()
        {
            foreach (Sound sound in SettingsManager.Cache)
            {
                sound.HotKey = new KeyBind();
                sound.HotKey.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Play sound for Hotkeys.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private static void PlaySound(object sender, HotkeyEventArgs e)
        {
            Log.Debug($"Trying to Play sound by Keybind : {e.Name}");

            try
            {
                using (SoundPlayer player = new SoundPlayer((UnmanagedMemoryStream)SoundManager.GetAudioFileFromResources(e.Name.Replace(' ', '_'))))
                {
                    player.Play();
                }
            }
            catch (Exception exception)
            {
                Log.Error("Can not play Sound!", exception);
            }
        }
    }
}