using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateCharacterDefinitionRequestModel<T>
    {
        public string Id { get; set; }
        public List<T> UpdateDefinition { get; set; }
    }
}
