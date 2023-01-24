using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class LocationModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("x")]
        public string X { get; set; }
        [JsonPropertyName("y")]
        public string Y { get; set; }
        [JsonPropertyName("date")]
        public int Date { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
        [JsonPropertyName("year")]
        public int Year { get; set; }
        [JsonPropertyName("season")]
        public string Season { get; set; }
        [JsonPropertyName("events")]
        public List<string>? Events { get; set; }
    }
}
