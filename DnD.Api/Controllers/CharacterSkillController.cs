using AutoMapper;
using DnD.Api.CustomAttributes;
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
    public class CharacterSkillController : ControllerBase
    {
        private readonly CharacterRepository _characterRepository;
        private readonly CharacterSkillRepository _characterSkillRepository;
        private readonly ILogger<CharacterSkillController> _logger;
        private readonly IMapper _mapper;
        public CharacterSkillController(CharacterSkillRepository characterSkillRepository, ILogger<CharacterSkillController> logger, IMapper mapper, CharacterRepository characterRepository)
        {
            _characterSkillRepository = characterSkillRepository;
            _logger = logger;
            _mapper = mapper;
            _characterRepository = characterRepository;
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
                return BadRequest(ErrorResponseModel.NewError("character-skill/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterSkillRepository.GetBySkillIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("character-skill/get-one", "skill not found"));
                var response = _mapper.Map<Shared.Models.CharacterSkillModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-skill/get-one", ex));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCharacterSkillRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.CharacterId, cancellationToken);
                if(findCharacter is null) return NotFound(ErrorResponseModel.NewError("character-skill/create", "character not found"));
                var responseRepo = await _characterSkillRepository.GetBySkillIdAndCharacterIdAsync(request.CharacterId, request.SkillId, cancellationToken);
                if (responseRepo is not null) return BadRequest(ErrorResponseModel.NewError("character-skill/create", "skill already exists"));
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
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-skill/create", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCharacterSkillRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterSkillRepository.GetBySkillIdAsync(request.Id, cancellationToken);
                if (responseRepo is null)  return NotFound(ErrorResponseModel.NewError("character-skill/update", "skill not found"));
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
                return BadRequest(ErrorResponseModel.NewError("character-skill/update", ex));
            }
        }
    }
}
