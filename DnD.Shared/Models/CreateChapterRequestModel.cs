using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class CreateChapterRequestModel
    {
        public string Name { get; set; }
        public string Story { get; set; }
        public DateTime Date { get; set; }
    }
}
