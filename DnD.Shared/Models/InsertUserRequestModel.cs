using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class InsertUserRequestModel
    {
        public string? CharacterId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
