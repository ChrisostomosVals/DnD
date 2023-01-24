using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class GearModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("quantity")]
        public double Quantity { get; set; }
        [JsonPropertyName("weight")]
        public string Weight { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
}
