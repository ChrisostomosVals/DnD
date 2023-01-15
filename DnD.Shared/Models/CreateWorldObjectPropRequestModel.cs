using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class CreateWorldObjectPropRequestModel
    {
        public int WorldObjectId { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
    }
}
