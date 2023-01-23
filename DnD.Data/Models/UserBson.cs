using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class UserBson
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("roleId")]
        public string RoleId { get; set; }
        [BsonElement("characterId")]
        public string? CharacterId { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
    }
}
