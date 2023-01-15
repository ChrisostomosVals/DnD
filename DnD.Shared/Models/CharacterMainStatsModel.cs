using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class CharacterMainStatsModel
    {
        [JsonPropertyName("characterId")]
        public string CHARACTER_ID { get; set; }
        [JsonPropertyName("strength")]
        public int STRENGTH { get; set; }
        [JsonPropertyName("dexterity")]
        public int DEXTERITY { get; set; }
        [JsonPropertyName("constitution")]
        public int CONSTITUTION { get; set; }
        [JsonPropertyName("wisdom")]
        public int WISDOM { get; set; }
        [JsonPropertyName("charisma")]
        public int CHARISMA { get; set; }
        [JsonPropertyName("intelligence")]
        public int INTELLIGENCE { get; set; }
        [JsonPropertyName("level")]
        public int LEVEL { get; set; }
        [JsonPropertyName("healthPoints")]
        public int HEALTH_POINTS { get; set; }
    }
}
