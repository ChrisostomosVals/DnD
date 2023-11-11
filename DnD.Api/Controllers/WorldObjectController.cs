using AutoMapper;
using DnD.Api.CustomAttributes;
using DnD.Api.Extensions;
using DnD.Data.Models;
using DnD.Data.Repositories;
using DnD.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System.IO;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorldObjectController : ControllerBase
    {
        private readonly WorldObjectRepository _worldObjectRepository;
        private readonly ILogger<WorldObjectController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public WorldObjectController(WorldObjectRepository worldObjectRepository, ILogger<WorldObjectController> logger, IMapper mapper, IConfiguration configuration)
        {
            _worldObjectRepository = worldObjectRepository;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldObjectRepository.GetAsync(cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.WorldObjectModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldObjectRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("world-object/get-one", "object not found"));
                var response = _mapper.Map<WorldObjectModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/get-one", ex));
            }
        }
        [HttpGet("{id}/images")]
        public async Task<IActionResult> GetImages(string id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldObjectRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("world-object/get-one", "object not found"));
                var response = _mapper.Map<WorldObjectModel>(responseRepo);
                ImagesUriModel responseImage = new ImagesUriModel
                {
                    Id = id,
                    Images = new List<ImageInfoModel>()
                };
                response.Properties?.ForEach(prop =>
                {
                    if (prop.Name == "image")
                    {
                        if (System.IO.File.Exists(prop.Value))
                        {
                            using (var image = Image.Load(prop.Value))
                            {
                                int width = image.Width;
                                int height = image.Height;

                                const int maxWidth = 800;
                                if (width > maxWidth)
                                {
                                    image.Mutate(x => x.Resize(maxWidth, 0));
                                    width = maxWidth;
                                    height = (int)(image.Height * ((float)maxWidth / image.Width));
                                }
                                IImageFormat imageFormat;
                                switch (Path.GetExtension(prop.Value).ToLower())
                                {
                                    case ".jpg":
                                    case ".jpeg":
                                    default:
                                        imageFormat = JpegFormat.Instance;
                                        break;
                                    case ".png":
                                        imageFormat = PngFormat.Instance;
                                        break;
                                }
                                using (var memoryStream = new MemoryStream())
                                {
                                    image.Save(memoryStream, imageFormat);
                                    byte[] imageBytes = memoryStream.ToArray();
                                    string base64Image = Convert.ToBase64String(imageBytes);

                                    string imageUrl = $"data:image/{imageFormat.DefaultMimeType.ToLower()};base64,{base64Image}";


                                    ImageInfoModel imageInfo = new ImageInfoModel
                                    {
                                        Url = imageUrl,
                                        Width = width,
                                        Height = height,
                                        Path = prop.Value
                                    };

                                    responseImage.Images.Add(imageInfo);
                                }
                            }
                        }
                    }
                });
                return Ok(responseImage);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/get-images", ex));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorldObjectRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var newItem = new WorldObjectModel
                {
                    Name = request.Name,
                    Type = request.Type,
                    Description = request.Description,
                    Properties = request.Properties is null ? new List<WorldObjectPropModel>() : request.Properties
                };
                var newItemMap = _mapper.Map<WorldObjectBson>(newItem);
                await _worldObjectRepository.InsertAsync(newItemMap, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/create", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateWorldObjectRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findItem = await _worldObjectRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findItem is null) return NotFound(ErrorResponseModel.NewError("world-object/update", "Item not found"));
                var newItem = new WorldObjectModel
                {
                    Id = request.Id,
                    Name = request.Name,
                    Type = request.Type,
                    Description = request.Description,
                    Properties = request.Properties is null ? new List<WorldObjectPropModel>() : request.Properties
                };
                var newItemMap = _mapper.Map<WorldObjectBson>(newItem);
                await _worldObjectRepository.UpdateAsync(newItemMap, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/update", ex));
            }
        }
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var worldObject = await _worldObjectRepository.GetByIdAsync(request.Id, cancellationToken);
                if (worldObject is null) return NotFound(ErrorResponseModel.NewError("world-object/upload-image", "world-object not found"));
                var filePath = _configuration.GetSection("FilePaths").GetValue<string>("Images");
                string newFullPath = "";
                int count = 1;
                var checkPath = Path.Combine(filePath, "world-object", worldObject.Name);
                if (!Directory.Exists(checkPath)) Directory.CreateDirectory(checkPath);
                string savePath = Path.Combine(filePath, "world-object", worldObject.Name, request.File.FileName);
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
                await request.File.CopyToAsync(stream);
                var properties = worldObject.Properties;
                if (properties is null) properties = new List<WorldObjectPropBson>();
                properties.Add(new WorldObjectPropBson
                {
                    Name = "image",
                    Value = newFullPath
                });
                await _worldObjectRepository.UpdatePropertiesAsync(worldObject.Id!, properties);
                return Ok(new { path = newFullPath });
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/upload-image", ex));
            }
        }
        [HttpDelete("images")]
        public async Task<IActionResult> DeleteImages(DeleteImagesRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var worldObject = await _worldObjectRepository.GetByIdAsync(request.Id, cancellationToken);
                if (worldObject is null) return NotFound(ErrorResponseModel.NewError("world-object/delete-images", "world-object not found"));
                List<string> propertiesToDelete = new List<string>();
                foreach (var path in request.Paths)
                {
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        propertiesToDelete.Add(path);
                    }
                }
                if (worldObject.Properties is not null)
                {
                    worldObject.Properties = worldObject.Properties.Where(prop => !propertiesToDelete.Contains(prop.Value)).ToList();
                }
                await _worldObjectRepository.UpdatePropertiesAsync(worldObject.Id!, worldObject.Properties!, cancellationToken);
                return Ok(new { paths = propertiesToDelete });
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/delete-images", ex));
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findItem = await _worldObjectRepository.GetByIdAsync(id, cancellationToken);
                if (findItem is null) return NotFound(ErrorResponseModel.NewError("world-object/delete", "Item not found"));
                await _worldObjectRepository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/delete", ex));
            }
        }
    }
}
