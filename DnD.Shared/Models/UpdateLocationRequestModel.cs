﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateLocationRequestModel : InsertLocationRequestModel
    {
        public string Id { get; set; }
    }
}
