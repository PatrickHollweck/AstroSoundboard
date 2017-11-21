// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 15:07
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindManager.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services
{
    using AstroSoundBoard.Models.DataModels;

    using NHotkey;
    using NHotkey.Wpf;

    public class KeybindManager
    {
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
                    // Eat. Exception for flow control what a genuine good idea...
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
                sound.HotKey.NotifyOfPropertyChange();
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