using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonIgnore]
        public string RoleId { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("characterId")]
        public string? CharacterId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
