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
    public class ClassCategoryController : ControllerBase
    {
        private readonly ClassCategoryRepository _classCategoryRepository;
        private readonly ILogger<ClassController> _logger;
        private readonly IMapper _mapper;
        public ClassCategoryController(ClassCategoryRepository classCategoryRepository, ILogger<ClassController> logger, IMapper mapper)
        {
            _classCategoryRepository = classCategoryRepository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _classCategoryRepository.GetAsync(cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.ClassCategoryModel>>(responseRepo);
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
                var responseRepo = await _classCategoryRepository.GetByIdAsync(id, cancellationToken);
                var response = _mapper.Map<Shared.Models.ClassCategoryModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
