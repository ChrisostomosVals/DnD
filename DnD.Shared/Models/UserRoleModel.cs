using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class UserRoleModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}
