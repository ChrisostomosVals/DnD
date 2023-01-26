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
        public bool Visible { get; set; }

    }
}
