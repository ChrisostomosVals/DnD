using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class InsertCharacterPropRequestModel
    {
        public string CharacterId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
    }
}
