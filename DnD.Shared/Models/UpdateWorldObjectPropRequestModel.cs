﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UpdateWorldObjectPropRequestModel
    {
        public int Id { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
    }
}