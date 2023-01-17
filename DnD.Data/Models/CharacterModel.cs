using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class CharacterModel
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public int CLASS_ID { get; set; }
        public string TYPE { get; set; }
        public int RACE_ID { get; set; }
        public string? GENDER { get; set; }
        public int LEVEL { get; set; }
        public int STRENGTH { get; set; }
        public int DEXTERITY { get; set; }
        public int INTELLIGENCE { get; set; }
        public int CONSTITUTION { get; set; }
        public int WISDOM { get; set; }
        public int CHARISMA { get; set; }
        public int ARMOR_CLASS { get; set; }
        public int FORTITUDE { get; set; }
        public int REFLEX { get; set; }
        public int WILL { get; set; }
        public int BASE_ATTACK_BONUS { get; set; }
        public int SPELL_RESISTANCE { get; set; }
        public string? SIZE { get; set; }
        public int MAX_HP { get; set; }
        public int CURRENT_HP { get; set; }
        public int SPEED { get; set; }
        public string? HAIR { get; set; }
        public string? EYES { get; set; }
        public int FLY { get; set; }
        public int SWIM { get; set; }
        public int CLIMB { get; set; }
        public int BURROW { get; set; }
        public int TOUCH { get; set; }
        public int FLAT_FOOTED { get; set; }
        public string? HOMELAND { get; set; }
        public string? DEITY { get; set; }
        public Single HEIGHT { get; set; }
        public Single WEIGHT { get; set; }
        public Single EXPERIENCE { get; set; }
        public string? AGE { get; set; }
        public string? SCHEME { get; set; }
    }
}
