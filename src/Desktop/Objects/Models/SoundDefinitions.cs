// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:07:2017 / 19:06
// Creation: 20:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundDefinitions.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Objects.Models
{
    using System.Collections.Generic;

    using AstroSoundBoard.Services;

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
            return SoundModel.GetModel(SoundManager.GetSound(Sound.Name));
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