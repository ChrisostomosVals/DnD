using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class CharacterBson
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("classId")]
        public string ClassId { get; set; }
        [BsonElement("raceId")]
        public string RaceId { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("gear")]
        public List<GearBson>? Gear { get; set; }
        [BsonElement("arsenal")]
        public List<ArsenalBson>? Arsenal { get; set; }
        [BsonElement("skills")]
        public List<SkillBson>? Skills { get; set; }
        [BsonElement("feats")]
        public List<string>? Feats { get; set; }
        [BsonElement("specialAbilities")]
        public List<string>? SpecialAbilities { get; set; }
        [BsonElement("stats")]
        public List<StatBson>? Stats { get; set; }
        [BsonElement("properties")]
        public List<PropertyBson>? Properties { get; set; }
        [BsonElement("visible")]
        public bool Visible { get; set; }
    }
}
