using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class RaceModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("name")]
        public string NAME { get; set; }
        [JsonPropertyName("categoryId")]
        public int CATEGORY_ID { get; set; }
    }
}
