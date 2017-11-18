// ****************************** Module Header ****************************** //
//
//
// Last Modified: 17:07:2017 / 18:07
// Creation: 17:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="JsonSoundModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Models
{
    using Newtonsoft.Json;

    public class JsonHotKey
    {
        [JsonProperty("Modifier")]
        public int Modifier { get; set; }

        [JsonProperty("Key")]
        public int Key { get; set; }

        [JsonProperty("HasAssignedKeybind")]
        public bool HasAssignedKeybind { get; set; }

        [JsonProperty("ModifierString")]
        public string ModifierString { get; set; }

        [JsonProperty("KeyString")]
        public string KeyString { get; set; }
    }

    public class JsonSoundModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("FileName")]
        public string FileName { get; set; }

        [JsonProperty("IsFavorite")]
        public string IsFavorite { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("VideoLink")]
        public string VideoLink { get; set; }

        [JsonProperty("HotKey")]
        public JsonHotKey HotKey { get; set; }
    }
}