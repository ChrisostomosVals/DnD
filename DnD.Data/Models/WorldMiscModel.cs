using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class WorldMiscModel
    {
        public int ID { get; set; }
        public string PROPERTY { get; set; }
        public string VALUE { get; set; }
        public int DEPEND_ID { get; set; }
        public string? DEPEND_LOCATION { get; set; }
    }
}
