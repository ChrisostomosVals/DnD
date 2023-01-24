using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class CreateWorldObjectRequestModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        public List<WorldObjectPropModel>? Properties { get; set; }
    }
}
