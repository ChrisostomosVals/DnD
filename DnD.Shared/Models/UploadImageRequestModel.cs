using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Shared.Models
{
    public class UploadImageRequestModel
    {
        public string Id { get; set; }
        public IFormFile File { get; set; }
    }
}
