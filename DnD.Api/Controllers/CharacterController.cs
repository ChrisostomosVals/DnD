using AutoMapper;
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
using Microsoft.IdentityModel.Tokens;

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
                var response = _mapper.Map<CharacterModel>(responseRepo);
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
        [HttpGet("{id}/gear/{gearId}")]
        public async Task<IActionResult> GetGearItem(string id, string gearId, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/get-gear-one", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/get-gear-one", "You do not have permission for this action"));
                }
                var gear = await _characterRepository.GetGearItemAsync(findCharacter.Id!, gearId, cancellationToken);
                if (gear is null) return NotFound(ErrorResponseModel.NewError("character/get-gear-one", "gear item not found"));
                var response = _mapper.Map<GearModel>(gear);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get-one", ex));
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
        [HttpGet("{id}/stats")]
        public async Task<IActionResult> GetStats(string id, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/get-stats", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/get-stats", "You do not have permission for this action"));
                }
                var stats = await _characterRepository.GetStatsAsync(findCharacter.Id!, cancellationToken);
                var response = _mapper.Map<IEnumerable<StatModel>>(stats);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/get-stats", ex));
            }
        }
        [HttpPost]
        [Authorize(Roles = ("GAME MASTER"))]
        public async Task<IActionResult> Create(CreateCharacterRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.Gear is not null)
                    foreach (var item in request.Gear) item.Id = Guid.NewGuid().ToString();
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
                if(findCharacter.Gear is not null)
                {
                    foreach (var item in request.UpdateDefinition)
                    {
                        var findItem = findCharacter.Gear.FirstOrDefault(g => g.Name == item.Name);
                        if (findItem is not null)
                        {
                            item.Quantity += findItem.Quantity;
                        }
                    }
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
        [HttpPut("gear/money/add")]
        public async Task<IActionResult> AddMoney(UpdateCharacterMoneyRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/money-add", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/money-add", "You do not have permission for this action"));
                }
                if (!findCharacter.Gear.IsNullOrEmpty())
                {
                   var findMoney = await _characterRepository.GetGearItemAsync(request.Id, request.GearId, cancellationToken);
                   var checkMoney = findCharacter.Gear!.FirstOrDefault(g => g.Name == "Money");
                   if(findMoney is not null)
                    {
                        findCharacter.Gear!.FirstOrDefault(g => g.Id == findMoney.Id)!.Quantity += request.Quantity;
                    }
                   else if (checkMoney is not null)
                    {
                        findCharacter.Gear!.FirstOrDefault(g => g.Name == "Money")!.Quantity += request.Quantity;
                    }
                    else
                    {
                        findCharacter.Gear!.Add(new GearBson
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Money",
                            Quantity = request.Quantity
                        });
                    }
                }
                else
                {
                    findCharacter.Gear = new List<GearBson>
                    {
                        new GearBson
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Money",
                            Quantity = request.Quantity
                        }
                    };
                }

                await _characterRepository.UpdateGearAsync(request.Id, findCharacter.Gear!, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/money-add", ex));
            }
        }
        [HttpPut("gear/money/remove")]
        public async Task<IActionResult> RemoveMoney(UpdateCharacterMoneyRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/money-remove", "character not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/money-remove", "You do not have permission for this action"));
                }
                if (findCharacter.Gear.IsNullOrEmpty()) return NotFound(ErrorResponseModel.NewError("character/money-remove", "no gear found"));
                var findMoney = await _characterRepository.GetGearItemAsync(request.Id, request.GearId, cancellationToken);
                if(findMoney is null) return NotFound(ErrorResponseModel.NewError("character/money-remove", "no money found"));
                if (findMoney.Quantity < request.Quantity) return BadRequest(ErrorResponseModel.NewError("character/update-gear", "not enough money"));
                findCharacter.Gear!.FirstOrDefault(g => g.Id == findMoney.Id)!.Quantity -= request.Quantity;
                await _characterRepository.UpdateGearAsync(request.Id, findCharacter.Gear!, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/update-gear", ex));
            }
        }
        [HttpPut("gear/transfer")]
        public async Task<IActionResult> TransferGear(TransferGearItemRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                var requestCharacter = await _characterRepository.GetByIdAsync(user.CharacterId, cancellationToken);
                if (requestCharacter is null) return BadRequest(ErrorResponseModel.NewError("character/tranfer-gear", "uset not bound to a character"));
                var findCharacter = await _characterRepository.GetByIdAsync(request.CharacterId, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/tranfer-gear", "character not found"));
                if (requestCharacter.Gear.IsNullOrEmpty()) return NotFound(ErrorResponseModel.NewError("character/tranfer-gear", "character gear not found"));
                var findItem = await _characterRepository.GetGearItemAsync(requestCharacter.Id!, request.GearId, cancellationToken);
                if (findItem is null) return NotFound(ErrorResponseModel.NewError("character/tranfer-gear", "item not found"));
                if (findItem.Quantity < request.Quantity) return BadRequest(ErrorResponseModel.NewError("character/tranfer-gear", "not enought items"));
                if (findItem.Quantity == request.Quantity) requestCharacter.Gear!.Remove(findItem);
                else requestCharacter.Gear!.FirstOrDefault(g => g.Id == findItem.Id)!.Quantity -= request.Quantity;
                if (findCharacter.Gear.IsNullOrEmpty())
                {
                    findCharacter.Gear = new List<GearBson>
                    {
                        new GearBson
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name =findItem.Name,
                            Quantity = request.Quantity
                        }
                    };
                }
                else
                {
                    if (findCharacter.Gear!.FirstOrDefault(g => g.Name == findItem.Name) is null) findCharacter.Gear!.Add(new GearBson
                    {
                        Id = request.GearId,
                        Name = findItem.Name,
                        Quantity = request.Quantity
                    });
                    else findCharacter.Gear!.FirstOrDefault(g => g.Name == findItem.Name)!.Quantity += request.Quantity;
                }
                await _characterRepository.UpdateGearAsync(findCharacter.Id!, findCharacter.Gear!, cancellationToken);
                await _characterRepository.UpdateGearAsync(requestCharacter.Id!, requestCharacter.Gear!, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/tranfer-gear", ex));
            }
        }
        [HttpPut("arsenal/add")]
        public async Task<IActionResult> EquipItem(UpdateCharacterArsenalRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/add-arsenal", "character not found"));
                var findGear = await _characterRepository.GetGearItemAsync(request.Id, request.GearId, cancellationToken);
                if (findGear is null) return NotFound(ErrorResponseModel.NewError("character/add-arsenal", "gear item not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/add-arsenal", "You do not have permission for this action"));
                }
                if (!findCharacter.Arsenal.IsNullOrEmpty())
                {
                    var checkExistingArsenal = findCharacter.Arsenal!.Where(a => a.GearId == request.GearId);
                    if (checkExistingArsenal.Count() >= findGear.Quantity) return BadRequest(ErrorResponseModel.NewError("character/add-arsenal", "items are already equipped"));
                    findCharacter.Arsenal!.Add(new ArsenalBson
                    {
                        GearId = request.GearId,
                        Type = request.Type,
                        Range = request.Range,
                        AttackBonus = request.AttackBonus,
                        Critical = request.Critical,
                        Damage = request.Damage
                    });
                }
                else
                {
                    findCharacter.Arsenal = new List<ArsenalBson>
                    {
                        new ArsenalBson
                        {
                            GearId = request.GearId,
                            Type = request.Type,
                            Range = request.Range,
                            AttackBonus = request.AttackBonus,
                            Critical = request.Critical,
                            Damage = request.Damage
                        }
                    };
                }
                await _characterRepository.UpdateArsenalAsync(request.Id, findCharacter.Arsenal, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/add-arsenal", ex));
            }
        }
        [HttpPut("arsenal/remove")]
        public async Task<IActionResult> UnEquipItem(UpdateCharacterArsenalRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/add-arsenal", "character not found"));
                var findGear = await _characterRepository.GetGearItemAsync(request.Id, request.GearId, cancellationToken);
                if (findGear is null) return NotFound(ErrorResponseModel.NewError("character/add-arsenal", "gear item not found"));
                if (!User.IsInRole("GAME MASTER"))
                {
                    var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                    if (request.Id != user.CharacterId) return Unauthorized(ErrorResponseModel.NewError("character/add-arsenal", "You do not have permission for this action"));
                }
                if (findCharacter.Arsenal.IsNullOrEmpty()) return NotFound(ErrorResponseModel.NewError("character/add-arsenal", "inventory is empty"));
                var findArsenal = findCharacter.Arsenal!.FirstOrDefault(a => a.GearId == request.GearId);
                if (findArsenal is null) return NotFound(ErrorResponseModel.NewError("character/add-arsenal", "item does not exist in inventory"));
                findCharacter.Arsenal!.Remove(findArsenal);
                await _characterRepository.UpdateArsenalAsync(request.Id, findCharacter.Arsenal, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character/add-arsenal", ex));
            }
        }
        [HttpPut("skills")]
        public async Task<IActionResult> UpdateSkills(UpdateCharacterDefinitionRequestModel<SkillModel> request, CancellationToken cancellationToken)
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
        public async Task<IActionResult> UpdateStats(UpdateCharacterDefinitionRequestModel<StatModel> request, CancellationToken cancellationToken)
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
        public async Task<IActionResult> UpdateProperties(UpdateCharacterDefinitionRequestModel<PropertyModel> request, CancellationToken cancellationToken)
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
        [Authorize(Roles = "GAME MASTER")]
        [HttpPut("{id}/visibility/{visible}")]
        public async Task<IActionResult> ChangeVisibility(string id, bool visible, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(id, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character/change-visibility", "character not found"));
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