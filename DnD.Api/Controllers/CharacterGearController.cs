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
        private readonly CharacterRepository _characterRepository;
        private readonly CharacterGearRepository _characterGearRepository;
        private readonly ILogger<CharacterGearController> _logger;
        private readonly IMapper _mapper;
        public CharacterGearController(CharacterGearRepository characterGearRepository, ILogger<CharacterGearController> logger, IMapper mapper, CharacterRepository characterRepository)
        {
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
                var responseRepo = await _characterGearRepository.GetAsync(characterId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.CharacterGearModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-gear/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterGearRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("character-gear/get-one", "gear not found"));
                var response = _mapper.Map<Shared.Models.CharacterGearModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-gear/get-one", ex));
            }
        }
        [HttpGet("{characterId}/money")]
        public async Task<IActionResult> GetMoney(string characterId, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterGearRepository.GetMoneyByCharacterIdAsync(characterId, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("character-gear/get-money", "money not found"));
                var response = _mapper.Map<Shared.Models.CharacterGearModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-gear/get-money", ex));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(InsertCharacterGearRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(request.CharacterId, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character-gear/insert", "character not found"));
                var newGear = new Shared.Models.CharacterGearModel
                {
                    CHARACTER_ID = request.CharacterId,
                    NAME = request.Name,
                    QUANTITY = request.Quantity,
                    WEIGHT = request.Weight
                };
                var newGearMapped = _mapper.Map<Data.Models.CharacterGearModel>(newGear);
                await _characterGearRepository.InsertItemAsync(newGearMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-gear/insert", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCharacterGearRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findGear = await _characterGearRepository.GetByIdAsync(request.Id, cancellationToken);
                if(findGear is null) return NotFound(ErrorResponseModel.NewError("character-gear/update", "gear not found"));
                var newGear = new Shared.Models.CharacterGearModel
                {
                    ID = request.Id,
                    NAME = request.Name,
                    QUANTITY = request.Quantity,
                    WEIGHT = request.Weight
                };
                var newGearMapped = _mapper.Map<Data.Models.CharacterGearModel>(newGear);
                await _characterGearRepository.UpdateItemAsync(newGearMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-gear/update", ex));
            }
        }
        [HttpPut("transfer")]
        public async Task<IActionResult> TransferItem(TransferGearItemRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findGear = await _characterGearRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findGear is null) return NotFound(ErrorResponseModel.NewError("character-gear/transfer", "gear not found"));
                var findCharacter = await _characterRepository.GetByIdAsync(request.CharacterId, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character-gear/transfer", "character not found"));
                var transferGear = new Shared.Models.CharacterGearModel
                {
                    ID = request.Id,
                    CHARACTER_ID = request.CharacterId,
                    NAME = request.Name,
                    QUANTITY = request.Quantity,
                    WEIGHT = request.Weight
                };
                var newGearMapped = _mapper.Map<Data.Models.CharacterGearModel>(transferGear);
                await _characterGearRepository.TransferItemAsync(newGearMapped, cancellationToken);
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-gear/update", ex));
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var findGear = await _characterGearRepository.GetByIdAsync(id, cancellationToken);
                if (findGear is null) return NotFound(ErrorResponseModel.NewError("character-gear/delete", "gear not found"));
                await _characterGearRepository.DeleteItemAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-gear/delete", ex));
            }
        }
    }
}
