using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class ArsenalModel
    {
        [JsonPropertyName("gearId")]
        public string GearId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("range")]
        public string Range { get; set; }
        [JsonPropertyName("attackBonus")]
        public string AttackBonus { get; set; }
        [JsonPropertyName("critical")]
        public string Critical { get; set; }
        [JsonPropertyName("damage")]
        public string Damage { get; set; }
    }
}
