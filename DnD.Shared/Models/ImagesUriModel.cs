using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnD.Shared.Models
{
    public class ImagesUriModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("images")]
        public List<ImageInfoModel>? Images { get; set; }
    }
}
