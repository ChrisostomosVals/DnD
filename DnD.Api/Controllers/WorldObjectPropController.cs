using AutoMapper;
using DnD.Api.CustomAttributes;
using DnD.Data.Repositories;
using DnD.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorldObjectPropController : ControllerBase
    {
        private readonly WorldObjectRepository _worldObjectRepository;
        private readonly WorldObjectPropRepository _worldObjectPropRepository;
        private readonly ILogger<WorldObjectPropController> _logger;
        private readonly IMapper _mapper;

        public WorldObjectPropController(WorldObjectPropRepository worldObjectPropRepository, ILogger<WorldObjectPropController> logger, IMapper mapper, WorldObjectRepository worldObjectRepository)
        {
            _worldObjectPropRepository = worldObjectPropRepository;
            _logger = logger;
            _mapper = mapper;
            _worldObjectRepository = worldObjectRepository;
        }
        [HttpGet("{objectId}/all")]
        public async Task<IActionResult> Get(int objectId, CancellationToken cancellationToken)
        {
            try
            {
                var findObject = await _worldObjectRepository.GetByIdAsync(objectId, cancellationToken);
                if (findObject is null) return NotFound(ErrorResponseModel.NewError("world-object-prop/get", "object not found"));
                var responseRepo = await _worldObjectPropRepository.GetAsync(objectId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.WorldObjectPropModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object-prop/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldObjectPropRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("world-object-prop/get-one", "property not found"));
                var response = _mapper.Map<Shared.Models.WorldObjectPropModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object-prop/get-one", ex));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateWorldObjectPropRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findObject = await _worldObjectRepository.GetByIdAsync(request.WorldObjectId, cancellationToken);
                if (findObject is null) return NotFound(ErrorResponseModel.NewError("world-object-prop/create", "object not found"));
                var newProp = new Shared.Models.WorldObjectPropModel
                {
                    WORLD_OBJECT_ID = request.WorldObjectId,
                    PROPERTY = request.Property,
                    VALUE = request.Value
                };
                var newPropMap = _mapper.Map<Data.Models.WorldObjectPropModel>(newProp);
                await _worldObjectPropRepository.InsertAsync(newPropMap, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object-prop/create", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateWorldObjectPropRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldObjectPropRepository.GetByIdAsync(request.Id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("world-object-prop/update", "object property not found"));
                var newProp = new Shared.Models.WorldObjectPropModel
                {
                    ID = request.Id,
                    PROPERTY = request.Property,
                    VALUE = request.Value
                };
                var newPropMap = _mapper.Map<Data.Models.WorldObjectPropModel>(newProp);
                await _worldObjectPropRepository.UpdateAsync(newPropMap, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object-prop/update", ex));
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldObjectPropRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("world-object-prop/delete", "object property not found"));
                await _worldObjectPropRepository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object-prop/delete", ex));
            }
        }
    }
}
