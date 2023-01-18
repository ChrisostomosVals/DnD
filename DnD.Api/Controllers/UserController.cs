using AutoMapper;
using DnD.Api.Extensions;
using DnD.Data.Repositories;
using DnD.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly UserRoleRepository _userRoleRepository;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public UserController(UserRepository userRepository, UserRoleRepository userRoleRepository, ILogger<UserController> logger, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpGet]
        [Authorize(Roles = "GAME MASTER")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var userIdentity = User.GetSubjectId();
                var user = await _userRepository.GetByIdAsync(userIdentity, cancellationToken);
                if (user is null) return NotFound(ErrorResponseModel.NewError("user/fetch-all", "user not found"));

                var userRole = await _userRoleRepository.GetAsync(user.ROLE_ID, cancellationToken);
                var responseRepo = await _userRepository.GetAsync(userRole.ROLE, cancellationToken);
                
                var response = _mapper.Map<IEnumerable<Shared.Models.UserModel>>(responseRepo);
                foreach (var userResponse in response)
                {
                    var role = await _userRoleRepository.GetAsync(userResponse.ROLE_ID);
                    userResponse.ROLE = role.ROLE;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "GAME MASTER")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id, cancellationToken);
                if (user is null) return NotFound(ErrorResponseModel.NewError("user/fetch-one", "user not found"));

                var userRole = await _userRoleRepository.GetAsync(user.ROLE_ID, cancellationToken);
                var responseRepo = await _userRepository.GetByIdAsync(id, cancellationToken);
                var response = _mapper.Map<Shared.Models.UserModel>(responseRepo);
                response.ROLE = userRole.ROLE;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("user/fetch-one", ex));
            }
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile(CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                if (user is null) return NotFound(ErrorResponseModel.NewError("user/profile", "user not found"));

                var userRole = await _userRoleRepository.GetAsync(user.ROLE_ID, cancellationToken);
                var responseRepo = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                var response = _mapper.Map<Shared.Models.UserModel>(responseRepo);
                response.ROLE = userRole.ROLE;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("user/profile", ex));
            }
        }
        [HttpPost]
        [Authorize(Roles = "GAME MASTER")]
        public async Task<IActionResult> Insert(InsertUserRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var checkEmail = await _userRepository.CheckEmailAsync(request.Email, cancellationToken);
                if (checkEmail) return BadRequest(ErrorResponseModel.NewError("user/insert", "email already exists"));
                var newUser = new Data.Models.UserModel
                {
                    ID = Guid.NewGuid().ToString(),
                    CHARACTER_ID = request.CharacterId,
                    NAME = request.Name,
                    EMAIL = request.Email,
                    PASSWORD = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };
                await _userRepository.InsertAsync(newUser, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("user/insert", ex));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var findUser = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
                if (findUser is null) return NotFound(ErrorResponseModel.NewError("user/update", "user not found"));
                if (findUser.EMAIL != request.Email)
                {
                    var checkEmail = await _userRepository.CheckEmailAsync(request.Email, cancellationToken);
                    if (checkEmail) return BadRequest(ErrorResponseModel.NewError("user/update", "email already exists"));
                }
                var updateUser = new Data.Models.UserModel
                {
                    ID = request.Id,
                    CHARACTER_ID = request.CharacterId,
                    NAME = request.Name,
                    EMAIL = request.Email,
                };
                await _userRepository.UpdateAsync(updateUser, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("user/update", ex));
            }
        }
        [HttpPut("{id}/changepassword")]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                if (!User.IsInRole("GAME MASTER"))
                {
                    var loggedInUser = User.GetSubjectId();
                    if (id != loggedInUser) return Unauthorized();
                }
                var user = await _userRepository.GetByIdAsync(id, cancellationToken);
                if (user is null) return NotFound(ErrorResponseModel.NewError("user/change-password", "user not found"));
                var isVerified = BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PASSWORD);
                if (!isVerified) return BadRequest(ErrorResponseModel.NewError("user/change-password", "old password does not match"));
                await _userRepository.ChangePasswordAsync(id, BCrypt.Net.BCrypt.HashPassword(request.NewPassword), cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("user/change-password", ex));
            }
        }
    }
}
