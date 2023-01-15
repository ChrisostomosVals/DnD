using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class InsertCharacterArsenalRequestModel
    {
        public int GearId { get; set; }
        public string CharacterId { get; set; }
        public string Type { get; set; }
        public string Range { get; set; }
        public double AttackBonus { get; set; }
        public string Damage { get; set; }
        public string Critical { get; set; }
    }
}
