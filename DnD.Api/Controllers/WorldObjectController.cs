using AutoMapper;
using DnD.Api.CustomAttributes;
using DnD.Data.Models;
using DnD.Data.Repositories;
using DnD.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public WorldObjectController(WorldObjectRepository worldObjectRepository, ILogger<WorldObjectController> logger, IMapper mapper)
        {
            _worldObjectRepository = worldObjectRepository;
            _logger = logger;
            _mapper = mapper;
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
                var response = _mapper.Map<Shared.Models.WorldObjectModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/get-one", ex));
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
