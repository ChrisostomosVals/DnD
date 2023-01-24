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
    public class LocationController : ControllerBase
    {
        private readonly LocationRepository _locationRepository;
        private readonly ILogger<LocationController> _logger;
        private readonly IMapper _mapper;

        public LocationController(ILogger<LocationController> logger, LocationRepository locationRepository, IMapper mapper)
        {
            _logger = logger;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool latest, CancellationToken cancellationToken)
        {
            try
            {
                if (latest)
                {
                    var responseRepo = await _locationRepository.GetLatestAsync(cancellationToken);
                    if (responseRepo == null) return NotFound(ErrorResponseModel.NewError("location/get-latest", "location not found"));
                    var response = _mapper.Map<LocationModel>(responseRepo);
                    return Ok(response);
                }
               else
                {
                    var responseRepo = await _locationRepository.GetAsync(cancellationToken);
                    var response = _mapper.Map<IEnumerable<LocationModel>>(responseRepo);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {

                var responseRepo = await _locationRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("location/get-one", "location not found"));
                var response = _mapper.Map<LocationModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location/get-one", ex));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(InsertLocationRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var newLocation = new LocationModel
                {
                    X = request.X,
                    Y = request.Y,
                    Date = request.Date,
                    Time = request.Time,
                    Year = request.Year,
                    Season = request.Season,
                    Events = request.Events is null ? new List<string>() : request.Events
                };
                var newLocationMap = _mapper.Map<LocationBson>(newLocation);
                await _locationRepository.InsertAsync(newLocationMap, cancellationToken); 
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location/create", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateLocationRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findLocation = await _locationRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findLocation is null) return NotFound(ErrorResponseModel.NewError("location/update", "location not found"));
                var newLocation = new LocationModel
                {
                    Id = request.Id,
                    X = request.X,
                    Y = request.Y,
                    Date = request.Date,
                    Time = request.Time,
                    Year = request.Year,
                    Season = request.Season,
                    Events = request.Events is null ? new List<string>() : request.Events
                };
                var newLocationMap = _mapper.Map<LocationBson>(newLocation);
                await _locationRepository.UpdateAsync(newLocationMap, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location/update", ex));
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {

                var responseRepo = await _locationRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("location/delete", "location not found"));
                await _locationRepository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location/delete", ex));
            }
        }
    }
}
