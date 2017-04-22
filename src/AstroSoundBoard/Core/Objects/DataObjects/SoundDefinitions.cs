// ****************************** Module Header ****************************** //
//
//
// Last Modified: 21:04:2017 / 23:45
// Creation: 17:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundDefinitions.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Objects.DataObjects.SoundDefinitionJsonTypes
{
    using Newtonsoft.Json;

    public class SoundAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
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
    }
}

namespace AstroSoundBoard.Core.Objects.DataObjects.SoundDefinition
{
    using System.Collections.Generic;

    using AstroSoundBoard.Core.Objects.DataObjects.SoundDefinitionJsonTypes;

    using Newtonsoft.Json;

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