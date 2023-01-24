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
        public string? Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("classId")]
        public string ClassId { get; set; }
        [BsonElement("raceId")]
        public string RaceId { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("gear")]
        public IList<GearBson>? Gear { get; set; }
        [BsonElement("arsenal")]
        public IList<ArsenalBson>? Arsenal { get; set; }
        [BsonElement("skills")]
        public IList<SkillBson>? Skills { get; set; }
        [BsonElement("feats")]
        public IList<string>? Feats { get; set; }
        [BsonElement("specialAbilities")]
        public IList<string>? SpecialAbilities { get; set; }
        [BsonElement("stats")]
        public IList<StatBson>? Stats { get; set; }
        [BsonElement("properties")]
        public IList<PropertyBson>? Properties { get; set; }
        [BsonElement("visible")]
        public bool Visible { get; set; }
    }
}
