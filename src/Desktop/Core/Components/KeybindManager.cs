// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:07:2017 / 19:06
// Creation: 24:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindManager.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System.Reflection;

    using AstroSoundBoard.Core.Objects.Models;

    using log4net;

    using NHotkey;
    using NHotkey.Wpf;

    public class KeybindManager
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void SetKeybinds()
        {
            SettingsManager.Cache.ForEach(sound => HotkeyManager.Current.Remove(sound.Name));

            foreach (SoundModel sound in SettingsManager.Cache)
            {
                sound.HotKey = sound.HotKey ?? new KeyBind();

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

        public static bool CheckDuplicate(SoundModel soundModel)
        {
            // Poor mans implementation.
            try
            {
                HotkeyManager.Current.AddOrReplace(soundModel.Name, soundModel.HotKey.Key, soundModel.HotKey.Modifier, PlaySound);
            }
            catch (HotkeyAlreadyRegisteredException)
            {
                return true;
            }

            return false;
        }

        public static void UnregisterAllKeybinds()
        {
            foreach (SoundModel sound in SettingsManager.Cache)
            {
                HotkeyManager.Current.Remove(sound.Name);
            }
        }

        public static void RemoveAllKeybindsFromSettings()
        {
            foreach (SoundModel sound in SettingsManager.Cache)
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
            SettingsManager.GetSound(e.Name).PlaySound();
        }
    }
}