using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class CharacterModel
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string TYPE { get; set; }
        public string? GENDER { get; set; }
        public int CLASS_ID { get; set; }
        public int RACE_ID { get; set; }
    }
}
