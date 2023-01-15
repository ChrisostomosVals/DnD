using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class CharacterGearModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("name")]
        public string NAME { get; set; }
        [JsonPropertyName("characterId")]
        public string CHARACTER_ID { get; set; }
        [JsonPropertyName("quantity")]
        public Single QUANTITY { get; set; }
        [JsonPropertyName("weight")]
        public Single WEIGHT { get; set; }
    }
}
