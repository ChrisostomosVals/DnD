using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class WorldObjectModel
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string TYPE { get; set; }    
        public string? DESCRIPTION { get; set; }
    }
}
