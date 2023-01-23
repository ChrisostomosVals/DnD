using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class LocationBson
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("x")]
        public string X { get; set; }
        [BsonElement("y")]
        public string Y { get; set; }
        [BsonElement("date")]
        public int Date { get; set; }
        [BsonElement("year")]
        public int Year { get; set; }
        [BsonElement("season")]
        public string Season { get; set; }
        [BsonElement("events")]
        public List<string>? Events { get; set; }
    }
}
