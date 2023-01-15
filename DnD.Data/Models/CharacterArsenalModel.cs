using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class CharacterArsenalModel
    {
        public int ID { get; set; }
        public int GEAR_ID { get; set; }
        public string CHARACTER_ID { get; set; }
        public string TYPE { get; set; }
        public string RANGE { get; set; }
        public double ATTACK_BONUS { get; set; }
        public string DAMAGE { get; set; }
        public string CRITICAL { get; set; }
    }
}
