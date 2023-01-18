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
    public class RaceController : ControllerBase
    {
        private readonly RaceRepository _raceRepository;
        private readonly RaceCategoryRepository _raceCategoryRepository;
        private readonly ILogger<ClassController> _logger;
        private readonly IMapper _mapper;
        public RaceController(ILogger<ClassController> logger, RaceRepository raceRepository, IMapper mapper, RaceCategoryRepository raceCategoryRepository)
        {
            _logger = logger;
            _raceRepository = raceRepository;
            _mapper = mapper;
            _raceCategoryRepository = raceCategoryRepository;
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
                return BadRequest(ErrorResponseModel.NewError("race/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _raceRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("race/get-one", "race not found"));
                var response = _mapper.Map<Shared.Models.RaceModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("race/get-one", ex));
            }
        }
        [HttpGet("{categoryId}/category")]
        public async Task<IActionResult> GetByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            try
            {
                var findCategory = await _raceCategoryRepository.GetByIdAsync(categoryId, cancellationToken);
                if (findCategory is null) return NotFound(ErrorResponseModel.NewError("race/get-by-category", "category not found"));
                var responseRepo = await _raceRepository.GetByCategoryIdAsync(categoryId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.RaceModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("race/get-by-category", ex));
            }
        }
    }
}
