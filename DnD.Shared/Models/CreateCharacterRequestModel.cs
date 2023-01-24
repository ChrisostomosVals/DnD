using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class CreateCharacterRequestModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string ClassId { get; set; }
        public string RaceId { get; set; }
        public List<ArsenalModel> Arsenal { get; set; }
        public List<GearModel> Gear { get; set; }
        public List<SkillModel> Skills { get; set; }
        public List<string> Feats { get; set; }
        public List<string> SpecialAbilities { get; set; }
        public List<StatModel> Stats { get; set; }
        public List<PropertyModel>? Properties { get; set; }
        public bool Visible { get; set; }

    }
}
