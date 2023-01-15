using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class SkillModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("name")]
        public string NAME { get; set; }
        [JsonPropertyName("description")]
        public string? DESCRIPTION { get; set; }
    }
}
