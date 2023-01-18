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
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
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
                var newItem = new Shared.Models.WorldObjectModel
                {
                    NAME = request.Name,
                    TYPE = request.Type,
                    DESCRIPTION = request.Description
                };
                var newItemMap = _mapper.Map<Data.Models.WorldObjectModel>(newItem);
                await _worldObjectRepository.CreateAsync(newItemMap, cancellationToken);
                return NoContent();
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
                var newItem = new Shared.Models.WorldObjectModel
                {
                    ID = request.Id,
                    NAME = request.Name,
                    TYPE = request.Type,
                    DESCRIPTION = request.Description
                };
                var newItemMap = _mapper.Map<Data.Models.WorldObjectModel>(newItem);
                await _worldObjectRepository.Update(newItemMap, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/update", ex));
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var findItem = await _worldObjectRepository.GetByIdAsync(id, cancellationToken);
                if (findItem is null) return NotFound(ErrorResponseModel.NewError("world-object/delete", "Item not found"));
                await _worldObjectRepository.Delete(id, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-object/delete", ex));
            }
        }
    }
}
