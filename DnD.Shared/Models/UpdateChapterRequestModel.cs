using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateChapterRequestModel : CreateChapterRequestModel
    {
        public string Id { get; set; }
    }
}
