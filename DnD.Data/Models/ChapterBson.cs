﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class ChapterBson
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("story")]
        public string Story { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
