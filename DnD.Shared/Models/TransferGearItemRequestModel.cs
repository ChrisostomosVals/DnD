using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class TransferGearItemRequestModel
    {
        public string CharacterId { get; set; }
        public List<GearModel> Gear { get; set; }
    }
}
