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
    public class LocationEventController : ControllerBase
    {
        private readonly LocationEventRepository _locationEventRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ILogger<LocationEventController> _logger;
        private readonly IMapper _mapper;

        public LocationEventController(ILogger<LocationEventController> logger, LocationEventRepository locationEventRepository, LocationRepository locationRepository, IMapper mapper)
        {
            _logger = logger;
            _locationEventRepository = locationEventRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }
        [HttpGet("{locationId}/all")]
        public async Task<IActionResult> Get(int locationId, CancellationToken cancellationToken)
        {
            try
            {
                var location = await _locationRepository.GetByIdAsync(locationId, cancellationToken);
                if (location is null) return NotFound(ErrorResponseModel.NewError("location-event/get", "location not found"));
                var responseRepo = await _locationEventRepository.GetAsync(locationId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.LocationEventModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location-event/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _locationEventRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("location-event/get-one", "location not found"));
                var response = _mapper.Map<Shared.Models.LocationEventModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location-event/get-one", ex));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(InsertLocationEventRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findLocation = await _locationEventRepository.GetByIdAsync(request.LocationId, cancellationToken);
                if (findLocation is null) return NotFound(ErrorResponseModel.NewError("location-event/create", "location not found"));
                var newLocationEvent = new Shared.Models.LocationEventModel
                {
                    LOCATION_ID = request.LocationId,
                    DESCRIPTION = request.Description
                };
                var newLocationEventMap = _mapper.Map<Data.Models.LocationEventModel>(newLocationEvent);
                await _locationEventRepository.CreateAsync(newLocationEventMap, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location-event/create", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateLocationEventRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findLocation = await _locationEventRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findLocation is null) return NotFound(ErrorResponseModel.NewError("location-event/update", "location event not found"));
                var newLocationEvent = new Shared.Models.LocationEventModel
                {
                    ID = request.Id,
                    DESCRIPTION = request.Description
                };
                var newLocationEventMap = _mapper.Map<Data.Models.LocationEventModel>(newLocationEvent);
                await _locationEventRepository.UpdateAsync(newLocationEventMap, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location-event/update", ex));
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _locationEventRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("location-event/delete", "location event not found"));
                await _locationEventRepository.DeleteAsync(id, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location-event/delete", ex));
            }
        }
    }
}
