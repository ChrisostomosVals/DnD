using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class CreateCharacterRequestModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int ClassId { get; set; }
        public int RaceId { get; set; }
        public string? Gender { get; set; }
    }
}
