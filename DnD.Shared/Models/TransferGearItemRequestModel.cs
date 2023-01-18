using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class TransferGearItemRequestModel
    {
        public int Id { get; set; }
        public string CharacterId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Weight { get; set; }
    }
}
