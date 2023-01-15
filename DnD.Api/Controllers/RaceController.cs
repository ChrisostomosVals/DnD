using AutoMapper;
using DnD.Api.CustomAttributes;
using DnD.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKeyAuth]
    public class RaceController : ControllerBase
    {
        private readonly RaceRepository _raceRepository;
        private readonly ILogger<ClassController> _logger;
        private readonly IMapper _mapper;
        public RaceController(ILogger<ClassController> logger, RaceRepository raceRepository, IMapper mapper)
        {
            _logger = logger;
            _raceRepository = raceRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _raceRepository.GetAsync(cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.RaceModel>>(responseRepo);
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
                var responseRepo = await _raceRepository.GetByIdAsync(id, cancellationToken);
                var response = _mapper.Map<Shared.Models.RaceModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{categoryId}/category")]
        public async Task<IActionResult> GetByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _raceRepository.GetByCategoryIdAsync(categoryId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.RaceModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
