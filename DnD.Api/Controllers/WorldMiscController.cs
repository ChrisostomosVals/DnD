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
    public class WorldMiscController : ControllerBase
    {
        private readonly WorldObjectRepository _worldObjectRepository;
        private readonly WorldMiscRepository _worldMiscRepository;
        private readonly ILogger<WorldMiscController> _logger;
        private readonly IMapper _mapper;

        public WorldMiscController(WorldMiscRepository worldMiscRepository, ILogger<WorldMiscController> logger, IMapper mapper, WorldObjectRepository worldObjectRepository)
        {
            _worldMiscRepository = worldMiscRepository;
            _logger = logger;
            _mapper = mapper;
            _worldObjectRepository = worldObjectRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldMiscRepository.GetAsync(cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.WorldMiscModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-miscellanious/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldMiscRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("world-miscellanious/get-one", "item not found"));
                var response = _mapper.Map<Shared.Models.WorldMiscModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-miscellanious/get-one", ex));
            }
        }
        [HttpGet("{dependId}/depend")]
        public async Task<IActionResult> GetByDependId(int dependId, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldMiscRepository.GetByDependIdAsync(dependId, cancellationToken);
                if (responseRepo.Count() is 0) return NotFound(ErrorResponseModel.NewError("world-miscellanious/get-depended-items", "items not found"));
                var response = _mapper.Map<IEnumerable<Shared.Models.WorldMiscModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-miscellanious/get-depended-items", ex));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(InsertWorldMiscRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var newItem = new Shared.Models.WorldMiscModel
                {
                    DEPEND_ID = request.DependId,
                    DEPEND_LOCATION = request.DependLocation,
                    PROPERTY = request.Property,
                    VALUE= request.Value
                };
                var newItemMap = _mapper.Map<Data.Models.WorldMiscModel>(newItem);
                await _worldMiscRepository.InsertAsync(newItemMap, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-miscellanious/get-depended-items", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateWorldMiscRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findItem = await _worldMiscRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findItem is null) return NotFound(ErrorResponseModel.NewError("world-miscellanious/update", "item not found"));
                var newItem = new Shared.Models.WorldMiscModel
                {
                    ID = request.Id,
                    PROPERTY = request.Property,
                    VALUE = request.Value
                };
                var newItemMap = _mapper.Map<Data.Models.WorldMiscModel>(newItem);
                await _worldMiscRepository.UpdateAsync(newItemMap, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("world-miscellanious/update", ex));
            }
        }
    }
}
