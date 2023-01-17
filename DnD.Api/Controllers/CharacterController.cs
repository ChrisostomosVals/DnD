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
                IEnumerable<Data.Models.CharacterModel> responseRepo = type switch
                {
                    "hero" => await _characterRepository.GetHeroAsync(cancellationToken),
                    "npc" => await _characterRepository.GetNpcAsync(cancellationToken),
                    "hostile" => await _characterRepository.GetHostileAsync(cancellationToken),
                    "boss" => await _characterRepository.GetBossAsync(cancellationToken),
                    _ => await _characterRepository.GetAsync(cancellationToken),
                };
                var response = _mapper.Map<IEnumerable<Shared.Models.CharacterModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound("Character not Found");
                var response = _mapper.Map<Shared.Models.CharacterModel>(responseRepo);
                var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                var scheme = response.Scheme;
                if (!User.IsInRole("GAME MASTER") && scheme is not null)
                {
                    foreach (PropertyInfo propertyInfo in scheme.GetType().GetProperties())
                    {
                        var value = propertyInfo.GetValue(scheme, null);
                        if (!Convert.ToBoolean(value))
                        {
                            var prop = response.GetType().GetProperty(propertyInfo.Name.ToUpper());
                            if (prop is null)
                            {
                                switch (propertyInfo.Name)
                                {
                                    case "ArmorClass":
                                        prop = response.GetType().GetProperty("ARMOR_CLASS");
                                        break;
                                    case "BaseAttackBonus":
                                        prop = response.GetType().GetProperty("BASE_ATTACK_BONUS");
                                        break;
                                    case "SpellResistance":
                                        prop = response.GetType().GetProperty("SPELL_RESISTANCE");
                                        break;
                                    case "MaxHp":
                                        prop = response.GetType().GetProperty("MAX_HP");
                                        break;
                                    case "CurrentHp":
                                        prop = response.GetType().GetProperty("CURRENT_HP");
                                        break;
                                    case "FlatFooted":
                                        prop = response.GetType().GetProperty("FLAT_FOOTED");
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (prop.PropertyType == typeof(bool)) prop.SetValue(response, false);
                            else if (prop.PropertyType == typeof(string)) prop.SetValue(response, null);
                            else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(Single)) prop.SetValue(response, 0);
                        }
                    }
                }


                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                    ID = Guid.NewGuid().ToString(),
                    NAME = request.Name,
                    CLASS_ID = request.ClassId,
                    GENDER = request.Gender,
                    TYPE = request.Type,
                    LEVEL = request.Level,
                    RACE_ID = request.RaceId,
                    STRENGTH = request.Strength,
                    DEXTERITY = request.Dexterity,
                    INTELLIGENCE = request.Intelligence,
                    CONSTITUTION = request.Constitution,
                    WISDOM = request.Wisdom,
                    CHARISMA = request.Charisma,
                    ARMOR_CLASS = request.ArmorClass,
                    FORTITUDE = request.Fortitude,
                    REFLEX = request.Reflex,
                    WILL = request.Will,
                    BASE_ATTACK_BONUS = request.BaseAttackBonus,
                    SPELL_RESISTANCE = request.SpellResistance,
                    SIZE = request.Size,
                    MAX_HP = request.MaxHp,
                    CURRENT_HP = request.CurrentHp,
                    SPEED = request.Speed,
                    HAIR = request.Hair,
                    EYES = request.Eyes,
                    FLY = request.Fly,
                    SWIM = request.Swim,
                    CLIMB = request.Climb,
                    BURROW = request.Burrow,
                    TOUCH = request.Touch,
                    FLAT_FOOTED = request.FlatFooted,
                    HOMELAND = request.Homeland,
                    DEITY = request.Deity,
                    HEIGHT = request.Height,
                    WEIGHT = request.Weight,
                    EXPERIENCE = request.Experience,
                    AGE = request.Age,
                    SCHEME = request.Scheme is null ? null : JsonSerializer.Serialize(request.Scheme)
                };
                var newCharacterMapped = _mapper.Map<Data.Models.CharacterModel>(newCharacter);
                await _characterRepository.CreateAsync(newCharacterMapped, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCharacterRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound("Character not Found");
                if (!User.IsInRole("GAME MASTER"))
                {
                    var loggedInUser = User.GetSubjectId();
                    var user = await _userRepository.GetByIdAsync(loggedInUser, cancellationToken);
                    if (request.Id != user.CHARACTER_ID) return Unauthorized("You do not have permission for this action");
                    request.Scheme = findCharacter.SCHEME switch
                    {
                        _ => null,
                    };
                }
                var newCharacter = new CharacterModel
                {
                    ID = request.Id,
                    NAME = request.Name,
                    CLASS_ID = request.ClassId,
                    GENDER = request.Gender,
                    TYPE = request.Type,
                    LEVEL = request.Level,
                    RACE_ID = request.RaceId,
                    STRENGTH = request.Strength,
                    DEXTERITY = request.Dexterity,
                    INTELLIGENCE = request.Intelligence,
                    CONSTITUTION = request.Constitution,
                    WISDOM = request.Wisdom,
                    CHARISMA = request.Charisma,
                    ARMOR_CLASS = request.ArmorClass,
                    FORTITUDE = request.Fortitude,
                    REFLEX = request.Reflex,
                    WILL = request.Will,
                    BASE_ATTACK_BONUS = request.BaseAttackBonus,
                    SPELL_RESISTANCE = request.SpellResistance,
                    SIZE = request.Size,
                    MAX_HP = request.MaxHp,
                    CURRENT_HP = request.CurrentHp,
                    SPEED = request.Speed,
                    HAIR = request.Hair,
                    EYES = request.Eyes,
                    FLY = request.Fly,
                    SWIM = request.Swim,
                    CLIMB = request.Climb,
                    BURROW = request.Burrow,
                    TOUCH = request.Touch,
                    FLAT_FOOTED = request.FlatFooted,
                    HOMELAND = request.Homeland,
                    DEITY = request.Deity,
                    HEIGHT = request.Height,
                    WEIGHT = request.Weight,
                    EXPERIENCE = request.Experience,
                    AGE = request.Age,
                    SCHEME = JsonSerializer.Serialize(request.Scheme)
                };
                var newCharacterMapped = _mapper.Map<Data.Models.CharacterModel>(newCharacter);
                await _characterRepository.UpdateAsync(newCharacterMapped, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "GAME MASTER")]
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound("Character not Found");
                await _characterRepository.DeleteAsync(id, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}