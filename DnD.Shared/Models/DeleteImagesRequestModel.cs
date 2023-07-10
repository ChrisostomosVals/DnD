using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class DeleteImagesRequestModel
    {
        public string Id { get; set; }
        public string[] Paths { get; set; }
    }
}
