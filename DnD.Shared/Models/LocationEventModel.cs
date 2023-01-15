using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class LocationEventModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("locationId")]
        public int LOCATION_ID { get; set; }
        [JsonPropertyName("description")]
        public string DESCRIPTION { get; set; }
    }
}
