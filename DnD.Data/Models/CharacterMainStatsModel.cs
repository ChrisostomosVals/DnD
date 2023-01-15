using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class CharacterMainStatsModel
    {
        public string CHARACTER_ID { get; set; }
        public int STRENGTH { get; set; }
        public int DEXTERITY { get; set; }
        public int CONSTITUTION { get; set; }
        public int WISDOM { get; set; }
        public int CHARISMA { get; set; }
        public int INTELLIGENCE { get; set; }
        public int LEVEL { get; set; }
        public int HEALTH_POINTS { get; set; }
    }
}
