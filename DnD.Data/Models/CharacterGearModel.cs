using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class CharacterGearModel
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CHARACTER_ID { get; set; }
        public Single QUANTITY { get; set; }
        public Single WEIGHT { get; set; }
    }
}
