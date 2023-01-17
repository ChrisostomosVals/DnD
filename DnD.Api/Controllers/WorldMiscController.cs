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
        private readonly WorldMiscRepository _worldMiscRepository;
        private readonly ILogger<WorldMiscController> _logger;
        private readonly IMapper _mapper;

        public WorldMiscController(WorldMiscRepository worldMiscRepository, ILogger<WorldMiscController> logger, IMapper mapper)
        {
            _worldMiscRepository = worldMiscRepository;
            _logger = logger;
            _mapper = mapper;
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
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldMiscRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound("Item not Found");
                var response = _mapper.Map<Shared.Models.WorldMiscModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{dependId}/depend")]
        public async Task<IActionResult> GetByDependId(int dependId, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _worldMiscRepository.GetByDependIdAsync(dependId, cancellationToken);
                if (responseRepo.Count().Equals(0)) return NotFound("Request yielded no results");
                var response = _mapper.Map<IEnumerable<Shared.Models.WorldMiscModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateWorldMiscRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var newItem = new Shared.Models.WorldMiscModel
                {
                    ID = request.Id,
                    PROPERTY = request.Property,
                    VALUE = request.Value
                };
                var newItemMap = _mapper.Map<Data.Models.WorldMiscModel>(newItem);
                await _worldMiscRepository.UpdateAsync(newItemMap, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
