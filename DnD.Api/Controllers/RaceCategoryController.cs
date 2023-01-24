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
    public class RaceCategoryController : ControllerBase
    {
        private readonly RaceCategoryRepository _raceCategoryRepository;
        private readonly ILogger<RaceController> _logger;
        private readonly IMapper _mapper;
        public RaceCategoryController(RaceCategoryRepository raceCategoryRepository, ILogger<RaceController> logger, IMapper mapper)
        {
            _raceCategoryRepository = raceCategoryRepository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _raceCategoryRepository.GetAsync(cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.RaceCategoryModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("race-category/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _raceCategoryRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("race-category/get-one", "category not found"));
                var response = _mapper.Map<Shared.Models.RaceCategoryModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("race-category/get-one", ex));
            }
        }
    }
}
