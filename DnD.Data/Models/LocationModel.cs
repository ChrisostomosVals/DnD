using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class LocationModel
    {
        public int ID { get; set; }
        public string X_AXIS { get; set; }
        public string Y_AXIS { get; set; }
        public int DATE { get; set; }
        public int TIME { get; set; }
        public int YEAR { get; set; }
        public string SEASON { get; set; }

    }
}
