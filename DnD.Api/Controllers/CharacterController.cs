using AutoMapper;
using DnD.Data.Repositories;
using DnD.Data;
using DnD.Shared;
using Microsoft.AspNetCore.Mvc;
using DnD.Shared.Models;
using DnD.Api.CustomAttributes;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKeyAuth]
    public class CharacterController : ControllerBase
    {
        private readonly CharacterRepository _characterRepository;
        private readonly ILogger<CharacterController> _logger;
        private readonly IMapper _mapper;

        public CharacterController(ILogger<CharacterController> logger, CharacterRepository characterRepository, IMapper mapper)
        {
            _logger = logger;
            _characterRepository = characterRepository;
            _mapper = mapper;
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
                var response = _mapper.Map<Shared.Models.CharacterModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCharacterRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var newCharacter = new CharacterModel
                {
                    ID = Guid.NewGuid().ToString(),
                    NAME = request.Name,
                    CLASS_ID = request.ClassId,
                    TYPE = request.Type,
                    RACE_ID = request.RaceId,
                    GENDER = request.Gender
                };
                var newCharacterMapped = _mapper.Map<Data.Models.CharacterModel>(newCharacter);
                await _characterRepository.CreateAsync(newCharacterMapped, cancellationToken);
                return Ok();
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
                var newCharacter = new CharacterModel
                {
                    ID = request.Id,
                    NAME = request.Name,
                    CLASS_ID = request.ClassId,
                    TYPE = request.Type, 
                    RACE_ID = request.RaceId,
                    GENDER = request.Gender
                };
                var newCharacterMapped = _mapper.Map<Data.Models.CharacterModel>(newCharacter);
                await _characterRepository.UpdateAsync(newCharacterMapped, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}