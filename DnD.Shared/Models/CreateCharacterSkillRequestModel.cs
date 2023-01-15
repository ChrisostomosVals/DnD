using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class CreateCharacterSkillRequestModel
    {
        public string CharacterId { get; set; }
        public int SkillId { get; set; }
        public int AbilityMod { get; set; }
        public bool Trained { get; set; }
        public int Ranks { get; set; }
        public int MiscMod { get; set; }
    }
}
