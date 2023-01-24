using AutoMapper;
using DnD.Data.Repositories;
using DnD.Data;
using DnD.Shared;
using Microsoft.AspNetCore.Mvc;
using DnD.Shared.Models;
using DnD.Api.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using DnD.Api.Extensions;
using System.Text.Json;
using System.Reflection;
using DnD.Data.Models;
using Org.BouncyCastle.Asn1.Ocsp;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CharacterController : ControllerBase
    {
        private readonly CharacterRepository _characterRepository;
        private readonly UserRepository _userRepository;
        private readonly ILogger<CharacterController> _logger;
        private readonly IMapper _mapper;

        public CharacterController(ILogger<CharacterController> logger, CharacterRepository characterRepository, IMapper mapper, UserRepository userRepository)
        {
            _logger = logger;
            _characterRepository = characterRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? type, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<CharacterBson> responseRepo = type switch
                {
                    "hero" => await _characterRepository.GetHeroAsync(cancellationToken),
                    "npc" => await _characterRepository.GetNpcAsync(cancellationToken),
                    "hostile" => await _characterRepository.GetHostileAsync(cancellationToken),
                    "boss" => await _characterRepository.GetBossAsync(cancellationToken),
                    _ => await _characterRepository.GetAsync(cancellationToken),
                };
                var response = _mapper.Map<IEnumerable<CharacterModel>>(responseRepo);
                if(!User.IsInRole("GAME MASTER"))
                {
                    response = response.Where(r => r.Visible == true);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("character/get-one", "character not found"));
                var response = _mapper.Map<Shared.Models.CharacterModel>(responseRepo);
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (user.CharacterId != id && response.Stats is not null)
                    {
                        foreach (var item in response.Stats)
                        {
                            if (!item.Shown) response.Stats.Remove(item);
                        }
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get-one", ex));
            }
        }
        [HttpGet("{id}/money")]
        public async Task<IActionResult> GetMoney(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/get-gear", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/get-gear", "You do not have permission for this action"));
                }
                var gear = await _characterRepository.GetMoneyAsync(findCharacter.Id!, cancellationToken);
                var response = _mapper.Map<GearModel>(gear);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get-one", ex));
            }
        }
        [HttpGet("{id}/gear")]
        public async Task<IActionResult> GetGear(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/get-gear", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/get-gear", "You do not have permission for this action"));
                }
                var gear = await _characterRepository.GetGearAsync(findCharacter.Id!, cancellationToken);
                var response = _mapper.Map<IEnumerable<GearModel>>(gear);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get-gear", ex));
            }
        }
        [HttpGet("{id}/arsenal")]
        public async Task<IActionResult> GetArsenal(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/get-arsenal", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/get-arsenal", "You do not have permission for this action"));
                }
                var arsenal = await _characterRepository.GetArsenalAsync(findCharacter.Id!, cancellationToken);
                var response = _mapper.Map<IEnumerable<ArsenalModel>>(arsenal);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get-arsenal", ex));
            }
        }
        [HttpGet("{id}/properties")]
        public async Task<IActionResult> GetProperties(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/get-properties", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/get-properties", "You do not have permission for this action"));
                }
                var properties = await _characterRepository.GetPropertiesAsync(findCharacter.Id!, cancellationToken);
                var response = _mapper.Map<IEnumerable<PropertyModel>>(properties);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get-properties", ex));
            }
        }
        [HttpGet("{id}/skills")]
        public async Task<IActionResult> GetSkills(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/get-skills", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/get-skills", "You do not have permission for this action"));
                }
                var skills = await _characterRepository.GetSkillsAsync(findCharacter.Id!, cancellationToken);
                var response = _mapper.Map<IEnumerable<SkillModel>>(skills);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get-skills", ex));
            }
        }
        [HttpGet("{id}/feats")]
        public async Task<IActionResult> GetFeats(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/get-feats", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/get-feats", "You do not have permission for this action"));
                }
                var feats = await _characterRepository.GetFeatsAsync(findCharacter.Id!, cancellationToken);
                return Ok(feats);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get-feats", ex));
            }
        }
        [HttpGet("{id}/specialAbilities")]
        public async Task<IActionResult> GetSpecialAbilities(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/get-specialAbilities", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/get-specialAbilities", "You do not have permission for this action"));
                }
                var specialAbilities = await _characterRepository.GetSpecialAbilitiesAsync(findCharacter.Id!, cancellationToken);
                return Ok(specialAbilities);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get-specialAbilities", ex));
            }
        }
        [HttpPost]
        [Authorize(Roles = ("GAME MASTER"))]
        public async Task<IActionResult> Create(CreateCharacterRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var newCharacter = new CharacterModel
                {
                    Name = request.Name,
                    ClassId = request.ClassId,
                    Type = request.Type,
                    RaceId = request.RaceId,
                    Gear = request.Gear is null ? new List<GearModel>() : request.Gear,
                    Skills = request.Skills is null ? new List<SkillModel>() : request.Skills,
                    Arsenal = request.Arsenal is null ? new List<ArsenalModel>() : request.Arsenal,
                    Feats = request.Feats is null ? new List<string>() : request.Feats,
                    SpecialAbilities = request.SpecialAbilities is null ? new List<string>() : request.SpecialAbilities,
                    Stats = request.Stats is null ? new List<StatModel>() : request.Stats,
                    Properties = request.Properties is null ? new List<PropertyModel>() : request.Properties,
                    Visible = request.Visible,
                };
                var newCharacterMapped = _mapper.Map<CharacterBson>(newCharacter);
                await _characterRepository.CreateAsync(newCharacterMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/create", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCharacterRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/update", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/update", "You do not have permission for this action"));
                }
                var newCharacter = new CharacterModel
                {
                    Id = request.Id,
                    Name = request.Name,
                    ClassId = request.ClassId,
                    Type = request.Type,
                    RaceId = request.RaceId,
                };
                var newCharacterMapped = _mapper.Map<CharacterBson>(newCharacter);
                await _characterRepository.UpdateAsync(newCharacterMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/update", ex));
            }
        }
        [HttpPut("gear")]
        public async Task<IActionResult> UpdateGear(UpdateCharacterDefinitionRequestModel<GearModel> request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/update-gear", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/update-gear", "You do not have permission for this action"));
                }
                var newGearMapped = _mapper.Map<IList<GearBson>>(request.UpdateDefinition);
                await _characterRepository.UpdateGearAsync(request.Id, newGearMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/update-gear", ex));
            }
        }
        [HttpPut("arsenal")]
        public async Task<IActionResult> UpdateArsenal (UpdateCharacterDefinitionRequestModel<ArsenalModel> request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/update-arsenal", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/update-arsenal", "You do not have permission for this action"));
                }
                var newArsenalMapped = _mapper.Map<IList<ArsenalBson>>(request.UpdateDefinition);
                await _characterRepository.UpdateArsenalAsync(request.Id, newArsenalMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/update-arsenal", ex));
            }
        }
        [HttpPut("skills")]
        public async Task<IActionResult> UpdateArsenal(UpdateCharacterDefinitionRequestModel<SkillModel> request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/update-skills", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/update-skills", "You do not have permission for this action"));
                }
                var newSkillsMapped = _mapper.Map<IList<SkillBson>>(request.UpdateDefinition);
                await _characterRepository.UpdateSkillsAsync(request.Id, newSkillsMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/update-skills", ex));
            }
        }
        [HttpPut("feats")]
        public async Task<IActionResult> UpdateFeats(UpdateCharacterDefinitionRequestModel<string> request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/update-feats", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/update-feats", "You do not have permission for this action"));
                }
                var newFeatsMapped = _mapper.Map<IList<string>>(request.UpdateDefinition);
                await _characterRepository.UpdateFeatsAsync(request.Id, newFeatsMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/update-feats", ex));
            }
        }
        [HttpPut("specialAbilities")]
        public async Task<IActionResult> UpdateSpecialAbilities(UpdateCharacterDefinitionRequestModel<string> request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/update-specialAbilities", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/update-specialAbilities", "You do not have permission for this action"));
                }
                var newSpecialAbilitiesMapped = _mapper.Map<IList<string>>(request.UpdateDefinition);
                await _characterRepository.UpdateSpecialAbilitiesAsync(request.Id, newSpecialAbilitiesMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/update-specialAbilities", ex));
            }
        }
        [HttpPut("stats")]
        public async Task<IActionResult> UpdateStatsAbilities(UpdateCharacterDefinitionRequestModel<StatModel> request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/update-stats", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/update-stats", "You do not have permission for this action"));
                }
                var newStatsMapped = _mapper.Map<IList<StatBson>>(request.UpdateDefinition);
                await _characterRepository.UpdateStatsAsync(request.Id, newStatsMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/update-stats", ex));
            }
        }
        [HttpPut("properties")]
        public async Task<IActionResult> UpdatePropertiesAbilities(UpdateCharacterDefinitionRequestModel<PropertyModel> request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/update-properties", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/update-properties", "You do not have permission for this action"));
                }
                var newPropsMapped = _mapper.Map<IList<PropertyBson>>(request.UpdateDefinition);
                await _characterRepository.UpdatePropertiesAsync(request.Id, newPropsMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/update-properties", ex));
            }
        }
        [HttpPut("{id}/visibility/{visible}")]
        public async Task<IActionResult> ChangeVisibility(string id, bool visible, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/change-visibility", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/change-visibility", "You do not have permission for this action"));
                }
                await _characterRepository.ChangeVisibilityAsync(id, visible, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/change-visibility", ex));
            }
        }
        [Authorize(Roles = "GAME MASTER")]
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/delete", "character not found"));
                await _characterRepository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/delete", ex));
            }
        }
    }
}