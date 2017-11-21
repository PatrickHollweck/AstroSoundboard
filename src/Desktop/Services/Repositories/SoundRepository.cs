// ****************************** Module Header ****************************** //
//
//
// Last Modified: 21:11:2017 / 20:19
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundRepository.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Repositories
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using AstroSoundBoard.Models.DataModels;
    using AstroSoundBoard.Properties;
    using AstroSoundBoard.Services.Resource;

    using Newtonsoft.Json;

    public class SoundRepository
    {
        public static SoundDefinitions Cache { get; private set; }

        static SoundRepository()
        {
            Cache = JsonConvert.DeserializeObject<SoundDefinitions>(Resources.SoundDefinition);
        }

        /// <summary>
        /// Gets the SoundList
        /// </summary>
        /// <returns>Sound-list</returns>
        public static List<Definition> GetAll()
        {
            return Cache.SoundList;
        }

        /// <summary>
        /// Returns the cached sound via the SoundName
        /// </summary>
        /// <param name="soundName">Name of the Sound</param>
        /// <returns>Definition of the Sound</returns>
        public static Definition Get(string soundName)
        {
            return (from sound in GetAll()
                    where sound.Sound.Name == soundName
                    select sound).FirstOrDefault();
        }

        /// <summary>
        /// Gets a sound from the Resources
        /// </summary>
        /// <param name="value">Name of the Resource</param>
        /// <returns>the resource</returns>
        public static object GetAudioFileFromResources(string value)
        {
            // NOTE: The Resources in c# are managed in a dictionary like structure. If the Key (resource name) is known the Value (resource itself) is easily obtainable.
            return (
                       from DictionaryEntry item in ResourceService.GetResourceSet()
                       where item.Key.ToString() == value
                       select item.Value
                   ).FirstOrDefault();
        }
    }
}