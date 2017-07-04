// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 18:29
// Creation: 20:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundManager.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Resources;

    using AstroSoundBoard.Core.Objects.DataObjects.SoundDefinition;
    using AstroSoundBoard.Core.Objects.DataObjects.SoundDefinitionJsonTypes;
    using AstroSoundBoard.Core.Objects.Models;
    using AstroSoundBoard.Properties;

    using log4net;

    using Newtonsoft.Json;

    public static class SoundManager
    {
        // This variable get's assigned in the Startup Process in the SettingsManager class!
        public static SoundDefinitions Cache { get; set; }

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // I could have used a static constructor but I like having more control over when this will happen!
        public static void Init()
        {
            Log.Info("Starting in Sound Manager!");

            try
            {
                Cache = JsonConvert.DeserializeObject<SoundDefinitions>(Resources.SoundDefinition);
            }
            catch (Exception exception)
            {
                Log.Fatal("Can not deserialize the downloaded Json!", exception);
            }
        }

        /// <summary>
        /// Gets a sound from the Resources
        /// </summary>
        /// <param name="value">Name of the Resource</param>
        /// <returns>the resource</returns>
        public static object GetAudioFileFromResources(string value)
        {
            // NOTE: The Resources in c# are managed in a dictionary like structure. If the Key (resource name) is known the Value (resource itself) is easily obtainable.
            foreach (DictionaryEntry item in GetResourcesSet())
            {
                if (item.Key.ToString() == value)
                {
                    return item.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the Resources in the Project
        /// </summary>
        /// <returns>List of Resources in the App</returns>
        public static ResourceSet GetResourcesSet()
        {
            return Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        }

        /// <summary>
        /// Gets the SoundList
        /// </summary>
        /// <returns>Sound-list</returns>
        public static List<Definition> GetSounds()
        {
            return Cache.SoundList;
        }

        /// <summary>
        /// Returns the cached sound via the SoundName
        /// </summary>
        /// <param name="soundName">Name of the Sound</param>
        /// <returns>Definition of the Sound</returns>
        public static Definition GetSound(string soundName)
        {
            return (from sound in GetSounds() where sound.Sound.Name == soundName select sound).FirstOrDefault();
        }

        /// <summary>
        /// Returns a Models from a given sound Name
        /// </summary>
        /// <param name="soundName">Name of the Sound</param>
        /// <returns>SoundModel of the Sound</returns>
        public static SoundModel GetModel(string soundName)
        {
            var definition = GetSound(soundName);
            var model = new SoundModel
            {
                Description = definition.Info.Description,
                IsFavorite = SettingsManager.GetSound(definition.Sound.Name).IsFavorite,
                Name = definition.Sound.Name,
                VideoLink = definition.Info.VideoLink
            };

            return model;
        }
    }
}