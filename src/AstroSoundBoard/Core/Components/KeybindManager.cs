// ****************************** Module Header ****************************** //
//
//
// Last Modified: 08:05:2017 / 14:38
// Creation: 08:05:2017
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

    using AstroSoundBoard.Core.Objects.DataObjects;

    using log4net;

    using NHotkey;
    using NHotkey.Wpf;

    public class KeybindManager
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Sets all keybinds in the <see cref="SettingsManager.Cache"/>
        /// </summary>
        public static void SetKeybinds()
        {
            Log.Debug("Setting Keybinds...");

            foreach (Sound sound in SettingsManager.Cache)
            {
                if (sound.HotKey == null)
                {
                    sound.HotKey = new KeyBind();
                }

                try
                {
                    HotkeyManager.Current.Remove(sound.Name);
                    HotkeyManager.Current.AddOrReplace(sound.Name, sound.HotKey.Key, sound.HotKey.Modifier, true, PlaySound);
                }
                catch (HotkeyAlreadyRegisteredException)
                {
                    // This exception will occur alot and we will ignore it :)
                }
                catch (Exception exception)
                {
                    Log.Error($"HotKey error!", exception);
                }
            }
        }

        /// <summary>
        /// Checks if a keybind with that Signature already exists.
        /// </summary>
        /// <param name="sound"></param>
        /// <returns></returns>
        public static (bool status, string) CheckDuplicate(Sound sound)
        {
            (bool, string) returnType = (true, "Success");

            try
            {
                HotkeyManager.Current.AddOrReplace(sound.Name, sound.HotKey.Key, sound.HotKey.Modifier, true, PlaySound);
            }
            catch (HotkeyAlreadyRegisteredException)
            {
                // Thanks c# 7 for Tuples :)
                returnType = (false, $@"A Hotkey for {sound.HotKey.Modifier}+{sound.HotKey.Key} is already defined, in {GetSoundByKeybind(sound)?.Name}! Please choose another one! ");
            }
            catch
            {
                returnType = (false, "Sorry the Hotkey can not be set");
            }
            finally
            {
                HotkeyManager.Current.Remove(sound.Name);
            }

            return returnType;
        }

        /// <summary>
        /// Gets the Keybind with a given Signature
        /// </summary>
        /// <param name="sound"></param>
        /// <returns></returns>
        public static Sound GetSoundByKeybind(Sound sound)
        {
            foreach (Sound item in SettingsManager.Cache)
            {
                if (item.HotKey.Key == sound.HotKey.Key && item.HotKey.Modifier == sound.HotKey.Modifier)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Play sound for Hotkeys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PlaySound(object sender, HotkeyEventArgs e)
        {
            Log.Debug($"Trying to Play sound : {e.Name}");

            try
            {
                string name = e.Name;
                name = name.Replace(' ', '_');

                using (SoundPlayer player = new SoundPlayer((UnmanagedMemoryStream)SoundManager.GetAudioFileFromResources(name)))
                {
                    player.Play();
                }
            }
            catch (Exception exception)
            {
                Log.Error("Can not play Definition!", exception);
            }
        }

        /// <summary>
        /// Removes all keybinds from the cache
        /// </summary>
        public static void RemoveAllKeybinds()
        {
            for (int i = 0; i < SettingsManager.Cache.Count; i++)
            {
                SettingsManager.Cache[i].HotKey = new KeyBind();
                SettingsManager.ChangeSoundAndWrite(SettingsManager.Cache[i]);
            }

            SetKeybinds();
        }

        /// <summary>
        /// Removes all active Keybind handlers
        /// </summary>
        public static void RemoveAllKeybindMappings()
        {
            for (int i = 0; i < SettingsManager.Cache.Count; i++)
            {
                try
                {
                    HotkeyManager.Current.Remove(SettingsManager.Cache[i].Name);
                }
                catch
                {
                    // Do nothing since there will be many errors thrown.
                }
            }

            SetKeybinds();
        }

        /// <summary>
        /// Removes all Keybinds and all currently active Mappings to the Keybind.
        /// </summary>
        /// <param name="name"></param>
        public static void RemoveKeybindAndMappingByName(string name)
        {
            foreach (Sound sound in SettingsManager.Cache)
            {
                if (sound.Name == name)
                {
                    sound.HotKey = new KeyBind();
                }
            }

            try
            {
                HotkeyManager.Current.Remove(name);
            }
            catch (Exception exception)
            {
                Log.Error("Hotkey error!", exception);
            }
        }
    }
}