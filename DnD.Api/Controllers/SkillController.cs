using AutoMapper;
using DnD.Api.CustomAttributes;
using DnD.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SkillController : ControllerBase
    {
        private readonly SkillRepository _skillRepository;
        private readonly ILogger<SkillController> _logger;
        private readonly IMapper _mapper;
        public SkillController(SkillRepository skillRepository, ILogger<SkillController> logger, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _skillRepository.GetAsync(cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.SkillModel>>(responseRepo);
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
                var responseRepo = await _skillRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound("Skill Not Found");
                var response = _mapper.Map<Shared.Models.SkillModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
