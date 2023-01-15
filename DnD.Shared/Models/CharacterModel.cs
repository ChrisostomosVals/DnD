using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class CharacterModel
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("name")]
        public string NAME { get; set; }
        [JsonPropertyName("type")]
        public string TYPE { get; set; }
        [JsonPropertyName("classId")]
        public int CLASS_ID { get; set; }
        [JsonPropertyName("gender")]
        public string? GENDER { get; set; }
        [JsonPropertyName("raceId")]
        public int RACE_ID { get; set; }
    }
}
