using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class SkillModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("abilityMod")]
        public int AbilityMod { get; set; }
        [JsonPropertyName("trained")]
        public bool Trained { get; set; }
        [JsonPropertyName("ranks")]
        public int Ranks { get; set; }
        [JsonPropertyName("miscMod")]
        public int MiscMod { get; set; }
    }
}
