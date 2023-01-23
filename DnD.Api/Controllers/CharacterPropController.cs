using AutoMapper;
using DnD.Data.Repositories;
using DnD.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CharacterPropController : ControllerBase
    {
        private readonly CharacterRepository _characterRepository;
        private readonly CharacterPropRepository _characterPropRepository;
        private readonly ILogger<CharacterPropController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CharacterPropController(CharacterPropRepository characterPropRepository, ILogger<CharacterPropController> logger, IMapper mapper, CharacterRepository characterRepository, IConfiguration configuration)
        {
            _characterPropRepository = characterPropRepository;
            _logger = logger;
            _mapper = mapper;
            _characterRepository = characterRepository;
            _configuration = configuration;
        }
        [HttpGet("{characterId}/all")]
        public async Task<IActionResult> Get(string characterId, CancellationToken cancellationToken)
        {
            try
            {
                var findCharacter = await _characterRepository.GetByIdAsync(characterId, cancellationToken);
                if (findCharacter is null) return NotFound(ErrorResponseModel.NewError("character-prop/get-all", "character not found"));
                var responseRepo = await _characterPropRepository.GetAsync(characterId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.CharacterPropModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-prop/get-all", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _characterPropRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("character-prop/get-one", "property not found"));
                var response = _mapper.Map<Shared.Models.CharacterPropModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-arsenal/get-one", ex));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(InsertCharacterPropRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findGear = await _characterRepository.GetByIdAsync(request.CharacterId, cancellationToken);
                if (findGear is null) return NotFound(ErrorResponseModel.NewError("character-prop/insert", "character not found"));
                var newProp = new Shared.Models.CharacterPropModel
                {
                    CHARACTER_ID = request.CharacterId,
                    NAME = request.Name,
                    TYPE = request.Type,
                    VALUE = request.Value,
                    DESCRIPTION = request.Description
                };
                var newPropMapped = _mapper.Map<Data.Models.CharacterPropModel>(newProp);
                await _characterPropRepository.InsertAsync(newPropMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-arsenal/insert-item", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCharacterPropRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findItem = await _characterPropRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findItem is null) return NotFound(ErrorResponseModel.NewError("character-prop/update", "property not found"));
                var newItem = new Shared.Models.CharacterPropModel
                {
                    ID = request.Id,
                    NAME = request.Name,
                    TYPE = request.Type,
                    VALUE = request.Value,
                    DESCRIPTION = request.Description
                };
                var newItemMapped = _mapper.Map<Data.Models.CharacterPropModel>(newItem);
                await _characterPropRepository.UpdateAsync(newItemMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-prop/update", ex));
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var findItem = await _characterPropRepository.GetByIdAsync(id, cancellationToken);
                if (findItem is null) return NotFound(ErrorResponseModel.NewError("character-prop/delete", "property not found"));
                await _characterPropRepository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("character-prop/delete", ex));
            }
        }
    }
}
