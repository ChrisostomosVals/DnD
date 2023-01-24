using DnD.Data.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
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
        public string? Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("classId")]
        public string ClassId { get; set; }
        [JsonPropertyName("raceId")]
        public string RaceId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("gear")]
        public List<GearModel>? Gear { get; set; }
        [JsonPropertyName("arsenal")]
        public List<ArsenalModel>? Arsenal { get; set; }
        [JsonPropertyName("skills")]
        public List<SkillModel>? Skills { get; set; }
        [JsonPropertyName("feats")]
        public List<string>? Feats { get; set; }
        [JsonPropertyName("specialAbilities")]
        public List<string>? SpecialAbilities { get; set; }
        [JsonPropertyName("stats")]
        public List<StatModel>? Stats { get; set; }
        [JsonPropertyName("properties")]
        public List<PropertyModel>? Properties { get; set; }
        [JsonPropertyName("visible")]
        public bool Visible { get; set; }
    }
}
