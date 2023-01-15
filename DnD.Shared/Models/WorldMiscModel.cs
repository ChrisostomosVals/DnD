using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class WorldMiscModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("property")]
        public string PROPERTY { get; set; }
        [JsonPropertyName("value")]
        public string VALUE { get; set; }
        [JsonPropertyName("dependId")]
        public int DEPEND_ID { get; set; }
        [JsonPropertyName("dependLocation")]
        public string? DEPEND_LOCATION { get; set; }
    }
}
