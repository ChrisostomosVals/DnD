using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
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
        [JsonPropertyName("level")]
        public int LEVEL { get; set; }
        [JsonPropertyName("strength")]
        public int STRENGTH { get; set; }
        [JsonPropertyName("dexterity")]
        public int DEXTERITY { get; set; }
        [JsonPropertyName("intelligence")]
        public int INTELLIGENCE { get; set; }
        [JsonPropertyName("constitution")]
        public int CONSTITUTION { get; set; }
        [JsonPropertyName("wisdom")]
        public int WISDOM { get; set; }
        [JsonPropertyName("charisma")]
        public int CHARISMA { get; set; }
        [JsonPropertyName("armorClass")]
        public int ARMOR_CLASS { get; set; }
        [JsonPropertyName("fortitude")]
        public int FORTITUDE { get; set; }
        [JsonPropertyName("reflex")]
        public int REFLEX { get; set; }
        [JsonPropertyName("will")]
        public int WILL { get; set; }
        [JsonPropertyName("baseAttackBonus")]
        public int BASE_ATTACK_BONUS { get; set; }
        [JsonPropertyName("spellResistance")]
        public int SPELL_RESISTANCE { get; set; }
        [JsonPropertyName("size")]
        public string? SIZE { get; set; }
        [JsonPropertyName("maxHp")]
        public int MAX_HP { get; set; }
        [JsonPropertyName("currentHp")]
        public int CURRENT_HP { get; set; }
        [JsonPropertyName("speed")]
        public int SPEED { get; set; }
        [JsonPropertyName("hair")]
        public string? HAIR { get; set; }
        [JsonPropertyName("eyes")]
        public string? EYES { get; set; }
        [JsonPropertyName("fly")]
        public int FLY { get; set; }
        [JsonPropertyName("swim")]
        public int SWIM { get; set; }
        [JsonPropertyName("climb")]
        public int CLIMB { get; set; }
        [JsonPropertyName("burrow")]
        public int BURROW { get; set; }
        [JsonPropertyName("touch")]
        public int TOUCH { get; set; }
        [JsonPropertyName("flatFooted")]
        public int FLAT_FOOTED { get; set; }
        [JsonPropertyName("homeland")]
        public string? HOMELAND { get; set; }
        [JsonPropertyName("deity")]
        public string? DEITY { get; set; }
        [JsonPropertyName("height")]
        public Single HEIGHT { get; set; }
        [JsonPropertyName("weight")]
        public Single WEIGHT { get; set; }
        [JsonPropertyName("experience")]
        public Single EXPERIENCE { get; set; }
        [JsonPropertyName("age")]
        public string? AGE { get; set; }
        [JsonIgnore]
        public string? SCHEME { get; set; }
        public SchemeModel? Scheme => (SCHEME is null || SCHEME == "all" || SCHEME == "none") ? null : JsonSerializer.Deserialize<SchemeModel>(SCHEME);
    }
}
