using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class CharacterMainStatsRequestModel
    {
        public string CharacterId { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int Intelligence { get; set; }
        public int Level{ get; set; }
        public int HealthPoints{ get; set; }
    }
}
