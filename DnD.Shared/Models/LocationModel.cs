using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class LocationModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("x")]
        public string X_AXIS { get; set; }
        [JsonPropertyName("y")]
        public string Y_AXIS { get; set; }
        [JsonPropertyName("date")]
        public int DATE { get; set; }
        [JsonPropertyName("time")]
        public int TIME { get; set; }
        [JsonPropertyName("year")]
        public int YEAR { get; set; }
        [JsonPropertyName("season")]
        public string SEASON { get; set; }
    }
}
