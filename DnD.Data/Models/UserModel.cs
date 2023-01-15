using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Models
{
    public class UserModel
    {
        public string ID { get; set; }
        public int ROLE_ID { get; set; }
        public string? CHARACTER_ID { get; set; }
        public string? NAME { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
    }
}
