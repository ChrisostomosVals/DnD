using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class InsertWorldMiscRequestModel
    {
        public string Property { get; set; }
        public string Value { get; set; }
        public int DependId { get; set; }
        public string? DependLocation { get; set; }
    }
}
