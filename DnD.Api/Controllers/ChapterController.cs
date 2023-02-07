using AutoMapper;
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
    public class ChapterController : ControllerBase
    {
        private readonly ChapterRepository _chapterRepository;
        private readonly ILogger<CharacterController> _logger;
        private readonly IMapper _mapper;
        public ChapterController(ChapterRepository chapterRepository, ILogger<CharacterController> logger, IMapper mapper)
        {
            _chapterRepository = chapterRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _chapterRepository.GetAsync(cancellationToken);
                var response = _mapper.Map<IEnumerable<ChapterModel>>(responseRepo);
                return Ok(response.OrderBy(r => r.Date));
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("chapter/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _chapterRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("chapter/get-one", "chapter not found"));
                var response = _mapper.Map<ChapterModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("chapter/get-one", ex));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateChapterRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var newChapter = new ChapterModel
                {
                    Name = request.Name,
                    Story = request.Story,
                    Date = request.Date
                };
                var newChapterMapped = _mapper.Map<ChapterBson>(newChapter);
                await _chapterRepository.CreateAsync(newChapterMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("chapter/create", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateChapterRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findChapter = await _chapterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findChapter is null) return NotFound(ErrorResponseModel.NewError("chapter/update", "chapter not found"));
                var newChapter = new ChapterModel
                {
                    Id = request.Id,
                    Name = request.Name,
                    Story = request.Story,
                    Date = request.Date
                };
                var newChapterMapped = _mapper.Map<ChapterBson>(newChapter);
                await _chapterRepository.UpdateAsync(newChapterMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("chapter/update", ex));
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findChapter = await _chapterRepository.GetByIdAsync(id, cancellationToken);
                if (findChapter is null) return NotFound(ErrorResponseModel.NewError("chapter/update", "chapter not found"));
                await _chapterRepository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("chapter/delete", ex));
            }
        }
    }
}
