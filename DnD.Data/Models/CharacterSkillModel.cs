using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class CharacterSkillModel
    {
        public int ID { get; set; }
        public string CHARACTER_ID { get; set; }
        public int SKILL_ID { get; set; }
        public int ABILITY_MOD { get; set; }
        public bool TRAINED { get; set; }
        public int MISC_MOD { get; set; }
        public int RANKS { get; set; }
    }
}
