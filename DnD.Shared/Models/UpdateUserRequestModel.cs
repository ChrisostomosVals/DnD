using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateUserRequestModel
    {
        public string Id { get; set; }
        public string? CharacterId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
    }
}
