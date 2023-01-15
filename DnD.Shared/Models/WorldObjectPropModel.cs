using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class WorldObjectPropModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("worldObjectId")]
        public int WORLD_OBJECT_ID { get; set; }
        [JsonPropertyName("property")]
        public string PROPERTY { get; set; }
        [JsonPropertyName("value")]
        public string VALUE{ get; set; }
    }
}
