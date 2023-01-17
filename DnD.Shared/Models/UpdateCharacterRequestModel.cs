using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateCharacterRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int ClassId { get; set; }
        public string? Gender { get; set; }
        public int RaceId { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int ArmorClass { get; set; }
        public int Fortitude { get; set; }
        public int Reflex { get; set; }
        public int Will { get; set; }
        public int BaseAttackBonus { get; set; }
        public int SpellResistance { get; set; }
        public string? Size { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int Speed { get; set; }
        public string? Hair { get; set; }
        public string? Eyes { get; set; }
        public int Fly { get; set; }
        public int Swim { get; set; }
        public int Climb { get; set; }
        public int Burrow { get; set; }
        public int Touch { get; set; }
        public int FlatFooted { get; set; }
        public string? Homeland { get; set; }
        public string? Deity { get; set; }
        public Single Height { get; set; }
        public Single Weight { get; set; }
        public Single Experience { get; set; }
        public string? Age { get; set; }
        public SchemeModel? Scheme { get; set; }
    }
}
