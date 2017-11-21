// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 18:46
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundDefinitions.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Models.DataModels
{
    using System.Collections.Generic;
    using AstroSoundBoard.Services.Repositories;

    using Newtonsoft.Json;

    public class SoundAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class InfoAttribute
    {
        [JsonProperty("VideoLink")]
        public string VideoLink { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }

    public class Definition
    {
        [JsonProperty("sound")]
        public SoundAttribute Sound { get; set; }

        [JsonProperty("info")]
        public InfoAttribute Info { get; set; }

        public SoundModel ToSoundModel()
        {
            return SoundModel.GetModel(SoundRepository.Get(Sound.Name));
        }
    }

    public class SoundDefinitions
    {
        public SoundDefinitions()
        {
            SoundList = new List<Definition>();
        }

        [JsonProperty("FileVersion")]
        public string FileVersion { get; set; }

        [JsonProperty("ParserVersion")]
        public string ParserVersion { get; set; }

        [JsonProperty("Sounds")]
        public List<Definition> SoundList { get; set; }
    }
}