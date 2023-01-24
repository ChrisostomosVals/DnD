using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class RaceBson
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("categoryId")]
        public string CategoryId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
