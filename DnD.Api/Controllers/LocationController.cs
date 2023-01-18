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
                    var response = _mapper.Map<Shared.Models.LocationModel>(responseRepo);
                    return Ok(response);
                }
               else
                {
                    var responseRepo = await _locationRepository.GetAsync(cancellationToken);
                    var response = _mapper.Map<IEnumerable<Shared.Models.LocationModel>>(responseRepo);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location/get", ex));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {

                var responseRepo = await _locationRepository.GetByIdAsync(id, cancellationToken);
                if (responseRepo is null) return NotFound(ErrorResponseModel.NewError("location/get-one", "location not found"));
                var response = _mapper.Map<Shared.Models.LocationModel>(responseRepo);
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
                var newLocation = new Shared.Models.LocationModel
                {
                    X_AXIS = request.X,
                    Y_AXIS = request.Y,
                    DATE = request.Date,
                    TIME = request.Time,
                    YEAR = request.Year,
                    SEASON = request.Season
                };
                var newLocationMap = _mapper.Map<Data.Models.LocationModel>(newLocation);
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
                var newLocation = new Shared.Models.LocationModel
                {
                    ID = request.Id,
                    X_AXIS = request.X,
                    Y_AXIS = request.Y,
                    DATE = request.Date,
                    TIME = request.Time,
                    YEAR = request.Year,
                    SEASON = request.Season
                };
                var newLocationMap = _mapper.Map<Data.Models.LocationModel>(newLocation);
                await _locationRepository.UpdateAsync(newLocationMap, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("location/update", ex));
            }
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
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
