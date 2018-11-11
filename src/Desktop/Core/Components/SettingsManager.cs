// ****************************** Module Header ****************************** //
//
//
// Last Modified: 17:07:2017 / 18:09
// Creation: 17:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SettingsManager.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using AstroSoundBoard.Core.Objects;
using AstroSoundBoard.Core.Objects.DataObjects;
using AstroSoundBoard.Core.Objects.Models;
using AstroSoundBoard.Core.Utils.Extensions;
using AstroSoundBoard.Properties;
using log4net;
using Newtonsoft.Json;

namespace AstroSoundBoard.Core.Components
{
    /// <summary>
    /// The SettingsManager is a class managing the SoundSettings.json file (C:\ProgramData\AstroKittySoundBoard\)<para/>
    /// The file is in Json format and for serialisation Json.NET is used.<para/>
    /// Contained in this Sound File is a list of all sounds in the Application.<para/>
    /// These Sounds are stored as a Sound object which describes the Sound with properties like "isFavorite" and "HotKey".<para/>
    /// If a property changes the file can get rewritten to the disk effectively saving the Settings the users has made to the Sounds.<para/>
    /// From there the Sounds can get read back from the file and the UI can get setup.
    /// </summary>
    public static class SettingsManager
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The Cache is a List of Sounds containing a list of all the Sounds. This list is used for interaction with other components and as a buffer.
        /// </summary>
        internal static List<SoundModel> Cache { get; set; }

        /// <summary>
        /// Initializes the Settings Manager.
        /// </summary>
        public static void Init()
        {
            Log.Debug("Starting the SettingsManager");

            if (!File.Exists(AppSettings.SoundSettingsFilePath))
            {
                Cache = new List<SoundModel>();

                foreach (var definition in SoundManager.Cache.SoundList)
                {
                    Cache.Add(SoundModel.fromDefinition(definition));
                }

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

                    List<JsonSoundModel> jsonModels = JsonConvert.DeserializeObject<List<JsonSoundModel>>(readText);

                    Cache = new List<SoundModel>();
                    foreach (JsonSoundModel model in jsonModels)
                    {
                        Cache.Add(SoundModel.fromJsonSoundModel(model));
                    }
                }
                catch (Exception exception)
                {
                    File.Delete(AppSettings.SoundSettingsFilePath);
                    Log.Error("Settings Manager initialization failed!", exception);
                }
            }

            Cache.RemoveAll(item => item.Name == "DummyItem");

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
            File.WriteAllText(AppSettings.SoundSettingsFilePath, JsonConvert.SerializeObject(Cache));
        }

        /// <summary>
        /// Registers a Sound into the Manager.
        /// </summary>
        /// <param name="soundModel">Sound to be registered</param>
        private static void Add(SoundModel soundModel)
        {
            Log.Debug("Registering Definition!");
            Cache.Add(soundModel);
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
                MessageBox.Show(
                    $"The settings file could not be read! Make sure you only have one instance of the soundboard running and restart the Application \n\n {exception.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                );

                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Gets a Sound from the Manager
        /// </summary>
        /// <param name="name">Sound name</param>
        /// <returns>The queried sound.</returns>
        public static SoundModel GetSound(string name)
        {
            foreach (SoundModel item in Cache)
            {
                if (item.Name == name.ToDisplayName())
                {
                    return item;
                }
            }

            Add(new SoundModel { Name = name, IsFavorite = JsonConvert.False });
            return GetSound(name);
        }

        /// <summary>
        /// Changes the Sound and writes it to the disk.
        /// </summary>
        /// <param name="soundModel">Sound to change</param>
        public static void Update(SoundModel soundModel)
        {
            Log.Debug($"Changing Definition of {soundModel.Name}");

            for (int i = 0; i < Cache.Count; i++)
            {
                if (Cache[i].Name == soundModel.Name)
                {
                    Cache[i] = soundModel;
                    Save();

                    return;
                }
            }

            Log.Error("NO MATCH!");
        }

        public static List<SoundModel> GetSounds()
        {
            return Cache;
        }
    }
}