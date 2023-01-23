using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class ArsenalBson
    {
        [BsonElement("gearId")]
        public string GearId { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("range")]
        public string Range { get; set; }
        [BsonElement("attackBonus")]
        public string AttackBonus { get; set; }
        [BsonElement("critical")]
        public string Critical { get; set; }
        [BsonElement("damage")]
        public string Damage { get; set; }
    }
}
