using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class CharacterSkillModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("characterId")]
        public string CHARACTER_ID { get; set; }
        [JsonPropertyName("skillId")]
        public int SKILL_ID { get; set; }
        [JsonPropertyName("abilityMod")]
        public int ABILITY_MOD { get; set; }
        [JsonPropertyName("trained")]
        public bool TRAINED { get; set; }
        [JsonPropertyName("miscMod")]
        public int MISC_MOD { get; set; }
        [JsonPropertyName("ranks")]
        public int RANKS { get; set; }

    }
}
