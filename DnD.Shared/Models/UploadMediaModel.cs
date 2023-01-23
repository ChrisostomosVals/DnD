using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UploadMediaModel
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
