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
    public class CharacterGearController : ControllerBase
    {
        private readonly CharacterGearRepository _characterGearRepository;
        private readonly ILogger<CharacterGearController> _logger;
        private readonly IMapper _mapper;
        public CharacterGearController(CharacterGearRepository characterGearRepository, ILogger<CharacterGearController> logger, IMapper mapper)
        {
            _characterGearRepository = characterGearRepository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet("{characterId}/all")]
        public async Task<IActionResult> Get(string characterId, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterGearRepository.GetAsync(characterId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.CharacterGearModel>>(responseRepo);
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
                var responseRepo = await _characterGearRepository.GetByIdAsync(id, cancellationToken);
                var response = _mapper.Map<Shared.Models.CharacterGearModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{characterId}/money")]
        public async Task<IActionResult> GetMoney(string characterId, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterGearRepository.GetMoneyByCharacterIdAsync(characterId, cancellationToken);
                var response = _mapper.Map<Shared.Models.CharacterGearModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(InsertCharacterGearRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var newGear = new Shared.Models.CharacterGearModel
                {
                    CHARACTER_ID = request.CharacterId,
                    NAME = request.Name,
                    QUANTITY = request.Quantity,
                    WEIGHT = request.Weight
                };
                var newGearMapped = _mapper.Map<Data.Models.CharacterGearModel>(newGear);
                await _characterGearRepository.InsertItem(newGearMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCharacterGearRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findGear = await _characterGearRepository.GetByIdAsync(request.Id, cancellationToken);
                if(findGear is null) return NotFound("Gear item does not exist");
                var newGear = new Shared.Models.CharacterGearModel
                {
                    ID = request.Id,
                    NAME = request.Name,
                    QUANTITY = request.Quantity,
                    WEIGHT = request.Weight
                };
                var newGearMapped = _mapper.Map<Data.Models.CharacterGearModel>(newGear);
                await _characterGearRepository.UpdateItem(newGearMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _characterGearRepository.DeleteItem(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
