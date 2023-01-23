using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class WorldObjectPropBson
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("value")]
        public string Value { get; set; }
    }
}
