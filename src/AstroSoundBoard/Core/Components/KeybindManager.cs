// ****************************** Module Header ****************************** //
//
//
// Last Modified: 01:05:2017 / 13:33
// Creation: 01:05:2017
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

        public static void SetKeybinds()
        {
            Log.Debug("Setting Keybinds...");

            foreach (Sound sound in SettingsManager.Cache)
            {
                if (sound.HotKey == null)
                {
                    continue;
                }

                try
                {
                    HotkeyManager.Current.Remove(sound.Name);
                    HotkeyManager.Current.AddOrReplace(sound.Name, sound.HotKey.Key, sound.HotKey.Modifier, true, PlaySound);
                }
                catch
                {
                    // Do nothing. There will be alot of errors but this is fine.
                }
            }
        }

        public static void RemoveAllKeybinds()
        {
            for (int i = 0; i < SettingsManager.Cache.Count; i++)
            {
                SettingsManager.Cache[i].HotKey = new KeyBind();
                SettingsManager.RewriteSound(SettingsManager.Cache[i]);
            }

            SetKeybinds();
        }

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

        public static (bool status, string) CheckDuplicate(Sound sound)
        {
            (bool, string) returnType = (true, "Success");

            try
            {
                HotkeyManager.Current.AddOrReplace(sound.Name, sound.HotKey.Key, sound.HotKey.Modifier, true, PlaySound);
            }
            catch (HotkeyAlreadyRegisteredException)
            {
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

        private static void PlaySound(object sender, HotkeyEventArgs e)
        {
            Log.Debug($"Trying to Play sound : {e.Name}");

            try
            {
                string name = e.Name;
                name = name.Replace(' ', '_');

                using (SoundPlayer player = new SoundPlayer((UnmanagedMemoryStream)SoundManager.Storage.GetAudioFileFromResources(name)))
                {
                    player.Play();
                }
            }
            catch (Exception exception)
            {
                Log.Error("Can not play Definition!", exception);
            }
        }

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

        public static void RemoveKeybindByName(string name)
        {
            foreach (Sound sound in SettingsManager.Cache)
            {
                if (sound.Name == name)
                {
                    sound.HotKey = new KeyBind();
                }
            }
        }

        public static void DisableAllKeybindsAndMappings()
        {
            foreach (Sound sound in SettingsManager.Cache)
            {
                try
                {
                    HotkeyManager.Current.Remove(sound.Name);
                }
                catch (Exception exception)
                {
                    Log.Error("Hotkey error!", exception);
                }
            }
        }
    }
}