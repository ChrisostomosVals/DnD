using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateWorldObjectRequestModel : CreateWorldObjectRequestModel
    {
        public string Id { get; set; }
    }
}
