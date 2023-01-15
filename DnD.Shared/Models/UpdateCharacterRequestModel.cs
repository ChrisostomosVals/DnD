using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateCharacterRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public int RaceId { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
    }
}
