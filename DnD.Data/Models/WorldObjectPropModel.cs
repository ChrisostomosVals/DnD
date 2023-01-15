using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class WorldObjectPropModel
    {
        public int ID { get; set; }
        public int WORLD_OBJECT_ID { get; set; }
        public string PROPERTY { get; set; }
        public string VALUE { get; set; }
    }
}
