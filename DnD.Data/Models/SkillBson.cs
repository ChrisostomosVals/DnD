using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class SkillBson
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("abilityMod")]
        public int AbilityMod { get; set; }
        [BsonElement("trained")]
        public bool Trained { get; set; }
        [BsonElement("ranks")]
        public int Ranks { get; set; }
        [BsonElement("miscMod")]
        public int MiscMod { get; set; }
    }
}
