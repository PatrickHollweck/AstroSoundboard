// ****************************** Module Header ****************************** //
//
//
// Last Modified: 11:05:2017 / 16:41
// Creation: 11:05:2017
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
    using System.Reflection;
    using System.Windows.Forms;

    using AstroSoundBoard.Core.Objects;
    using AstroSoundBoard.Core.Objects.DataObjects;
    using AstroSoundBoard.Properties;

    using log4net;

    using Newtonsoft.Json;

    public class SettingsManager
    {
        // The SettingsManager is a class managing the SoundSettings.json file (C:\ProgramData\AstroKittySoundBoard\). The file is in Json format and for serialisation and serialisation Json.NET is used. Contained in this File is a list of all sounds in the Application. These Sounds are stored as a Sound object which describes the Sound with properties like "isFavorite" and "HotKey" if a property changes the file can get rewritten to the disk effectively saving the Settings the users has made to the Sounds. From there the Sounds can get read back from the file and the UI can get setup.
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The Cache is a List of Sounds containing a list of all the Sounds. This list is used for interaction with other components and as a buffer.
        /// </summary>
        internal static List<Sound> Cache { get; set; }

        /// <summary>
        /// Initializes the Settings Manager.
        /// </summary>
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

            if (Settings.Default.EnableSoundHotKeys)
            {
                // When the Definitions are read in, the application can start setting up the Keybinds. (Keybinds are stored in the soundSettings.json!)
                KeybindManager.SetKeybinds();
            }
        }

        /// <summary>
        /// Creates a clean soundSettings file.
        /// </summary>
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

        /// <summary>
        /// Resets the Cache.
        /// </summary>
        /// <param name="loadSounds">Optionally reloads all sounds back from the File.</param>
        private static void ResetCache(bool loadSounds = false)
        {
            Cache = null;
            Cache = new List<Sound>();

            if (loadSounds)
            {
                Init();
            }
        }

        /// <summary>
        /// Registers a Sound into the Manager.
        /// </summary>
        /// <param name="sound">Sound to be registered</param>
        private static void RegisterSound(Sound sound)
        {
            Log.Debug("Registering Definition!");
            Cache.Add(sound);
            Save();
        }

        /// <summary>
        /// Writes the Sounds to the Disk.
        /// </summary>
        public static void Save()
        {
            Log.Debug("Saving sound settings to disk...");

            if (File.Exists(AppSettings.SoundSettingsFilePath))
            {
                File.Delete(AppSettings.SoundSettingsFilePath);
            }

            try
            {
                File.WriteAllText(AppSettings.SoundSettingsFilePath, JsonConvert.SerializeObject(Cache));
            }
            catch (UnauthorizedAccessException exception)
            {
                MessageBox.Show($"The settings file could not be read! Make sure you only have one instance of the soundboard running and restart the Application \n\n {exception.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Gets a Sound from the Manager
        /// </summary>
        /// <param name="name">Sound name</param>
        /// <returns>The queried sound.</returns>
        public static Sound GetSound(string name)
        {
            name = name.Replace(" ", "_");

            foreach (Sound item in Cache)
            {
                item.Name = item.Name.Replace(" ", "_");

                if (item.Name == name)
                {
                    return item;
                }

            }

            RegisterSound(new Sound { Name = name, IsFavorite = JsonConvert.False });
            return GetSound(name);
        }

        /// <summary>
        /// Changes the Sound and writes it to the disk.
        /// </summary>
        /// <param name="sound">Sound to change</param>
        public static void ChangeSound(Sound sound)
        {
            Log.Debug($"Changing Definition of {sound.Name}");

            sound.Name = sound.Name.Replace(' ', '_');

            for (int i = 0; i < Cache.Count; i++)
            {
                if (Cache[i].Name == sound.Name)
                {
                    sound.Name = sound.Name.Replace(" ", "_");

                    Log.Error($"SOUND NAME AFTER CHANGE: {sound.Name}");

                    Cache[i] = sound;
                    Save();

                    sound.Name = sound.Name.Replace("_", " ");

                    return;
                }
            }

            Log.Error("NO MATCH!");
        }
    }
}