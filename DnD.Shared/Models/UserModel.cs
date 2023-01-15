using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("roleId")]
        public int ROLE_ID { get; set; }
        [JsonPropertyName("role")]
        public string ROLE { get; set; }
        [JsonPropertyName("characterId")]
        public string? CHARACTER_ID { get; set; }
        [JsonPropertyName("name")]
        public string? NAME { get; set; }
        [JsonPropertyName("email")]
        public string EMAIL { get; set; }
        [JsonPropertyName("password")]
        [JsonIgnore]
        public string PASSWORD { get; set; }
    }
}
