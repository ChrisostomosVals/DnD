using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class CharacterArsenalModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("gearId")]
        public int GEAR_ID { get; set; }
        [JsonPropertyName("name")]
        public string NAME { get; set; }
        [JsonPropertyName("characterId")]
        public string CHARACTER_ID { get; set; }
        [JsonPropertyName("type")]
        public string TYPE { get; set; }
        [JsonPropertyName("range")]
        public string RANGE { get; set; }
        [JsonPropertyName("attackBonus")]
        public double ATTACK_BONUS { get; set; }
        [JsonPropertyName("damage")]
        public string DAMAGE { get; set; }
        [JsonPropertyName("critical")]
        public string CRITICAL { get; set; }
    }
}
