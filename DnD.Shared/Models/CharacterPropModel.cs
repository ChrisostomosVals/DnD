using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class CharacterPropModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("characterId")]
        public string CHARACTER_ID { get; set; }
        [JsonPropertyName("name")]
        public string? NAME { get; set; }
        [JsonPropertyName("value")]
        public string? VALUE { get; set; }
        [JsonPropertyName("type")]
        public string? TYPE { get; set; }
        [JsonPropertyName("description")]
        public string? DESCRIPTION { get; set; }
    }
}
