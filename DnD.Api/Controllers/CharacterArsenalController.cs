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
    public class CharacterArsenalController : ControllerBase
    {
        private readonly CharacterArsenalRepository _characterArsenalRepository;
        private readonly CharacterGearRepository _characterGearRepository;
        private readonly ILogger<CharacterArsenalController> _logger;
        private readonly IMapper _mapper;

        public CharacterArsenalController(CharacterArsenalRepository characterMainStatsRepository, CharacterGearRepository characterGearRepository, ILogger<CharacterArsenalController> logger, IMapper mapper)
        {
            _characterArsenalRepository = characterMainStatsRepository;
            _characterGearRepository = characterGearRepository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet("{characterId}/all")]
        public async Task<IActionResult> Get(string characterId, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterArsenalRepository.GetAsync(characterId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.CharacterArsenalModel>>(responseRepo);
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
                var responseRepo = await _characterArsenalRepository.GetByIdAsync(id, cancellationToken);
                var response = _mapper.Map<Shared.Models.CharacterArsenalModel>(responseRepo);
                var gearItem = await _characterGearRepository.GetByIdAsync(responseRepo.GEAR_ID, cancellationToken);
                response.NAME = gearItem.NAME;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertItem(InsertCharacterArsenalRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
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
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateItem(UpdateCharacterArsenalRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findItem = await _characterArsenalRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findItem is null) return NotFound("Item does not exists");
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
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteItem(int id, CancellationToken cancellationToken)
        {
            try
            {
                var findItem = await _characterArsenalRepository.GetByIdAsync(id, cancellationToken);
                if (findItem is null) return NotFound("Item does not exists");
                await _characterArsenalRepository.DeleteAsync(id, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
