using AutoMapper;
using DnD.Api.CustomAttributes;
using DnD.Data.Repositories;
using DnD.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKeyAuth]
    public class CharacterMainStatsController : ControllerBase
    {
        private readonly CharacterMainStatsRepository _characterMainStatsRepository;
        private readonly CharacterRepository _characterRepository;
        private readonly ILogger<CharacterMainStatsController> _logger;
        private readonly IMapper _mapper;

        public CharacterMainStatsController(CharacterMainStatsRepository characterMainStatsRepository, ILogger<CharacterMainStatsController> logger, IMapper mapper, CharacterRepository characterRepository)
        {
            _characterMainStatsRepository = characterMainStatsRepository;
            _logger = logger;
            _mapper = mapper;
            _characterRepository = characterRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterMainStatsRepository.GetAsync(cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.CharacterMainStatsModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{characterId}")]
        public async Task<IActionResult> GetById(string characterId, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterMainStatsRepository.GetByIdAsync(characterId, cancellationToken);
                if (findCharacter is null) return NoContent();
                var responseRepo = await _characterMainStatsRepository.GetByIdAsync(characterId, cancellationToken);
                var response = _mapper.Map<Shared.Models.CharacterMainStatsModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CharacterMainStatsRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findStats = await _characterMainStatsRepository.GetByIdAsync(request.CharacterId, cancellationToken);
                if (findStats is not null) return BadRequest("This character has already stats");
                var newCharStats = new Shared.Models.CharacterMainStatsModel
                {
                    CHARACTER_ID = request.CharacterId,
                    CHARISMA = request.Charisma,
                    CONSTITUTION = request.Constitution,
                    INTELLIGENCE = request.Intelligence,
                    DEXTERITY = request.Dexterity,
                    STRENGTH = request.Strength,
                    WISDOM = request.Wisdom,
                    HEALTH_POINTS = request.HealthPoints,
                    LEVEL = request.Level
                };
                var newCharStatsMapped = _mapper.Map<Data.Models.CharacterMainStatsModel>(newCharStats);
                await _characterMainStatsRepository.InsertAsync(newCharStatsMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(CharacterMainStatsRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var newCharStats = new Shared.Models.CharacterMainStatsModel
                {
                    CHARACTER_ID = request.CharacterId,
                    CHARISMA = request.Charisma,
                    CONSTITUTION = request.Constitution,
                    INTELLIGENCE = request.Intelligence,
                    DEXTERITY = request.Dexterity,
                    STRENGTH = request.Strength,
                    WISDOM = request.Wisdom,
                    HEALTH_POINTS = request.HealthPoints,
                    LEVEL = request.Level
                };
                var newCharStatsMapped = _mapper.Map<Data.Models.CharacterMainStatsModel>(newCharStats);
                await _characterMainStatsRepository.UpdateAsync(newCharStatsMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
