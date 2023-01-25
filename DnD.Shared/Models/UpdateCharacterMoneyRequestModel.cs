using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateCharacterMoneyRequestModel
    {
        public string Id { get; set; }
        public string? GearId { get; set; }
        public double Quantity { get; set; }
    }
}
