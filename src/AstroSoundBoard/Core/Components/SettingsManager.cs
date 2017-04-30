// ****************************** Module Header ****************************** //
//
//
// Last Modified: 30:04:2017 / 20:21
// Creation: 29:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SettingsManager.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using AstroSoundBoard.Core.Objects;
    using AstroSoundBoard.Core.Objects.DataObjects;

    using log4net;

    using Newtonsoft.Json;

    public class SettingsManager
    {
        internal static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal static List<Sound> Cache { get; set; }

        public static void Init()
        {
            Log.Debug("Starting the SettingsManager");

            if (!File.Exists(AppSettings.SoundSettingsFilePath))
            {
                CreateStandardFile();
            }
            else
            {
                try
                {
                    var readText = File.ReadAllText(AppSettings.SoundSettingsFilePath);

                    if (string.IsNullOrWhiteSpace(readText))
                    {
                        File.Delete(AppSettings.SoundSettingsFilePath);
                        CreateStandardFile();
                    }

                    Cache = JsonConvert.DeserializeObject<List<Sound>>(readText);
                }
                catch (Exception exception)
                {
                    File.Delete(AppSettings.SoundSettingsFilePath);
                    Log.Error("Something failed.", exception);
                }
            }

            KeybindManager.SetKeybinds();
        }

        private static void CreateStandardFile()
        {
            Sound stdObject = new Sound();

            Sound stdSounds = new Sound
            {
                Name = "DummyItem",
                IsFavorite = JsonConvert.False
            };

            ResetCache();
            Cache.Add(stdSounds);
            File.WriteAllText(AppSettings.SoundSettingsFilePath, JsonConvert.SerializeObject(stdObject));
        }

        private static void ResetCache()
        {
            Cache = null;
            Cache = new List<Sound>();
        }

        private static void RegisterSound(Sound sound)
        {
            Log.Debug("Registering Definition!");
            Cache.Add(sound);
            WriteSounds();
        }

        public static void WriteSounds()
        {
            Log.Debug("Writing sound...");

            if (File.Exists(AppSettings.SoundSettingsFilePath))
            {
                File.Delete(AppSettings.SoundSettingsFilePath);
            }

            File.WriteAllText(AppSettings.SoundSettingsFilePath, JsonConvert.SerializeObject(Cache));
        }

        public static Sound GetSound(string name)
        {
            foreach (Sound item in Cache)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            RegisterSound(new Sound { Name = name, IsFavorite = JsonConvert.False });
            return GetSound(name);
        }

        public static void RewriteSound(Sound sound)
        {
            if (sound.Name.Contains(" "))
            {
                sound.Name = sound.Name.Replace(' ', '_');
            }

            Log.Debug($"Changing Definition of {sound.Name}");
            for (int i = 0; i < Cache.Count; i++)
            {
                if (Cache[i].Name == sound.Name)
                {
                    Cache[i] = sound;
                    WriteSounds();
                    return;
                }
            }

            Log.Error("NO MATCH!");
        }
    }
}