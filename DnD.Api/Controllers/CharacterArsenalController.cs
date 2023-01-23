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
    public class CharacterArsenalController : ControllerBase
    {
        private readonly CharacterRepository _characterRepository;
        private readonly CharacterArsenalRepository _characterArsenalRepository;
        private readonly CharacterGearRepository _characterGearRepository;
        private readonly ILogger<CharacterArsenalController> _logger;
        private readonly IMapper _mapper;

        public CharacterArsenalController(CharacterArsenalRepository characterArsenalRepository, CharacterGearRepository characterGearRepository, ILogger<CharacterArsenalController> logger, IMapper mapper, CharacterRepository characterRepository)
        {
            _characterArsenalRepository = characterArsenalRepository;
            _characterGearRepository = characterGearRepository;
            _logger = logger;
            _mapper = mapper;
            _characterRepository = characterRepository;
        }
        [HttpGet("{characterId}/all")]
        public async Task<IActionResult> Get(string characterId, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(characterId, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character-arsenal/get-all", "character not found"));
                var responseRepo = await _characterArsenalRepository.GetAsync(characterId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.CharacterArsenalModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-arsenal/get-all",  ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterArsenalRepository.GetByIdAsync(id, cancellationToken);
                if(responseRepo is null ) return NotFound(ErrorResponseModel.NewError("character-arsenal/get-one", "item not found"));
                var response = _mapper.Map<Shared.Models.CharacterArsenalModel>(responseRepo);
                var gearItem = await _characterGearRepository.GetByIdAsync(responseRepo.GEAR_ID, cancellationToken);
                if (gearItem is null) return NotFound(ErrorResponseModel.NewError("character-arsenal/get-one", "gear not found"));
                response.NAME = gearItem.NAME;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-arsenal/get-one", ex));
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertItem(InsertCharacterArsenalRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findGear = await _characterGearRepository.GetByIdAsync(request.GearId, cancellationToken);
                if(findGear is null) return NotFound(ErrorResponseModel.NewError("character-arsenal/insert-item", "gear not found"));
                var newItem = new Shared.Models.CharacterArsenalModel
                {
                    CHARACTER_ID = request.CharacterId,
                    GEAR_ID = request.GearId,
                    ATTACK_BONUS = request.AttackBonus,
                    CRITICAL = request.Critical,
                    DAMAGE = request.Damage,
                    RANGE = request.Range,
                    TYPE = request.Type
                };
                var newItemMapped = _mapper.Map<Data.Models.CharacterArsenalModel>(newItem);
                await _characterArsenalRepository.InsertAsync(newItemMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-arsenal/insert-item", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateItem(UpdateCharacterArsenalRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findItem = await _characterArsenalRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findItem is null) return NotFound(ErrorResponseModel.NewError("character-arsenal/update-item", "item not found"));
                var findGear = await _characterGearRepository.GetByIdAsync(request.GearId, cancellationToken);
                if (findGear is null) return NotFound(ErrorResponseModel.NewError("character-arsenal/insert-item", "gear not found"));
                var newItem = new Shared.Models.CharacterArsenalModel
                {
                    ID = request.Id,
                    GEAR_ID = request.GearId,
                    ATTACK_BONUS = request.AttackBonus,
                    CRITICAL = request.Critical,
                    DAMAGE = request.Damage,
                    RANGE = request.Range,
                    TYPE = request.Type
                };
                var newItemMapped = _mapper.Map<Data.Models.CharacterArsenalModel>(newItem);
                await _characterArsenalRepository.UpdateAsync(newItemMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-arsenal/update-item", ex));
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteItem(int id, CancellationToken cancellationToken)
        {
            try
            {
                var findItem = await _characterArsenalRepository.GetByIdAsync(id, cancellationToken);
                if (findItem is null) return NotFound(ErrorResponseModel.NewError("character-arsenal/delete-item", "item not found"));
                await _characterArsenalRepository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-arsenal/delete-item", ex));
            }
        }
    }
}
