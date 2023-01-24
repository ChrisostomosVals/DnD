using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class WorldObjectBson
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
        [BsonElement("properties")]
        public IList<WorldObjectPropBson>? Properties { get; set; }
    }
}
