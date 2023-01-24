using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class GearBson
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("quantity")]
        public double Quantity { get; set; }
        [BsonElement("weight")]
        public string Weight { get; set; }
        [BsonElement("id")]
        public string Id { get; set; }
    }
}
