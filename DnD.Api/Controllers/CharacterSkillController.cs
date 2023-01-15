using AutoMapper;
using DnD.Api.CustomAttributes;
using DnD.Data.Models;
using DnD.Data.Repositories;
using DnD.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKeyAuth]
    public class CharacterSkillController : ControllerBase
    {
        private readonly CharacterSkillRepository _characterSkillRepository;
        private readonly ILogger<CharacterSkillController> _logger;
        private readonly IMapper _mapper;
        public CharacterSkillController(CharacterSkillRepository characterSkillRepository, ILogger<CharacterSkillController> logger, IMapper mapper)
        {
            _characterSkillRepository = characterSkillRepository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet("{characterId}/all")]
        public async Task<IActionResult> Get(string characterId, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterSkillRepository.GetByIdAsync(characterId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.CharacterSkillModel>>(responseRepo);
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
                var responseRepo = await _characterSkillRepository.GetBySkillIdAsync(id, cancellationToken);
                var response = _mapper.Map<Shared.Models.CharacterSkillModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCharacterSkillRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterSkillRepository.GetBySkillIdAndCharacterIdAsync(request.CharacterId, request.SkillId, cancellationToken);
                if (responseRepo is not null)
                    return BadRequest("Skill Already Exists");
                var newCharacterSkill = new Shared.Models.CharacterSkillModel
                {
                    CHARACTER_ID = request.CharacterId,
                    SKILL_ID = request.SkillId,
                    ABILITY_MOD = request.AbilityMod,
                    TRAINED = request.Trained,
                    RANKS = request.Ranks,
                    MISC_MOD = request.MiscMod
                };
                var newCharacterSkillMapped = _mapper.Map<Data.Models.CharacterSkillModel>(newCharacterSkill);
                await _characterSkillRepository.InsertSkill(newCharacterSkillMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCharacterSkillRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterSkillRepository.GetBySkillIdAsync(request.Id, cancellationToken);
                if (responseRepo is null)
                    return NotFound("Skill not found");
                var newCharacterSkill = new Shared.Models.CharacterSkillModel
                {
                    ID = request.Id,
                    RANKS = request.Ranks,
                    ABILITY_MOD = request.AbilityMod,
                    TRAINED = request.Trained,
                    MISC_MOD = request.MiscMod
                };
                var newCharacterSkillMapped = _mapper.Map<Data.Models.CharacterSkillModel>(newCharacterSkill);
                await _characterSkillRepository.UpdateSkill(newCharacterSkillMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
