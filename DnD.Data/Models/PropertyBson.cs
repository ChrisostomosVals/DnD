using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class PropertyBson
    {
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("value")]
        public string Value { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
    }
}
