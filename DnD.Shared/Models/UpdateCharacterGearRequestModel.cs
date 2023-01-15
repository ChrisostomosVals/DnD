using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateCharacterGearRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Single Quantity { get; set; }
        public Single Weight { get; set; }
    }
}
