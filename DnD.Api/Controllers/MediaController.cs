using DnD.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net;
using UploadStream;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MediaController : ControllerBase
    {
        private readonly ILogger<MediaController> _logger;
        private readonly IConfiguration _configuration;

        public MediaController(IConfiguration configuration, ILogger<MediaController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        [HttpGet("file/{path}")]
        public IActionResult Download(string path)
        {
            if (!System.IO.File.Exists(path)) return NotFound(ErrorResponseModel.NewError("media/download", "media not found"));
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            new FileExtensionContentTypeProvider().TryGetContentType(path, out var contentType);
            return File(fileStream, contentType);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadMediaRequestModel request)
        {
            try
            {
                var filePath = _configuration.GetSection("FilePaths").GetValue<string>("Images");
                string newFullPath = "";
                List<string> filePaths = new List<string>();
                foreach (var s in request.Files)
                {
                    int count = 1;
                    var checkPath = Path.Combine(filePath, request.Type, request.Name);
                    if (!Directory.Exists(checkPath)) Directory.CreateDirectory(checkPath);
                    string savePath = Path.Combine(filePath, request.Type, request.Name, s.FileName);
                    string fileNameOnly = Path.GetFileNameWithoutExtension(savePath);
                    string extension = Path.GetExtension(savePath);
                    string path = Path.GetDirectoryName(savePath)!;
                    newFullPath = savePath;
                    while (System.IO.File.Exists(newFullPath))
                    {
                        string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                        newFullPath = Path.Combine(path, tempFileName + extension);
                    }
                    using var stream = new FileStream(newFullPath, FileMode.Create);
                    await s.CopyToAsync(stream);
                    filePaths.Add(newFullPath);
                }

                return Ok(filePaths);
            }
            catch(Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("media-upload", ex));
            }
        }
        [HttpDelete("{path}/delete")]
        public async Task<IActionResult> Delete(string path)
        {
            try
            {
                if (!System.IO.File.Exists(path)) return NotFound(ErrorResponseModel.NewError("media/download", "media not found"));
                System.IO.File.Delete(path);
                return Ok(new { path });
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("media-delete", ex));
            }
        }
        [HttpGet("image/{path}")]
        public IActionResult GetImage(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(path);

                    string base64Image = Convert.ToBase64String(imageBytes);

                    string imageUrl = $"data:image/jpeg;base64,{base64Image}";

                    return Ok(new { url = imageUrl });
                }

                return NotFound(ErrorResponseModel.NewError("media-get-image", "image not found"));
            }
            catch(Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("media-get-image", ex));
            }
        }

    }
}
