using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateLocationRequestModel : InsertLocationRequestModel
    {
        public int Id { get; set; }
    }
}
