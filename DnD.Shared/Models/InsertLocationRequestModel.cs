using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class InsertLocationRequestModel
    {
        public string X { get; set; }
        public string Y { get; set; }
        public int Date { get; set; }
        public string Time { get; set; }
        public int Year { get; set; }
        public string Season { get; set; }
        public List<string>? Events { get; set; }
    }
}
