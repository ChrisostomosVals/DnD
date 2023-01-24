using AutoMapper;
using DnD.Api.Extensions;
using DnD.Data.Models;
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
                if (user is null) return NotFound(ErrorResponseModel.NewError("user/get-all", "user not found"));

                var userRole = await _userRoleRepository.GetByIdAsync(user.RoleId, cancellationToken);
                var responseRepo = await _userRepository.GetAsync(cancellationToken);
                
                var response = _mapper.Map<IEnumerable<UserModel>>(responseRepo);
                foreach (var userResponse in response)
                {
                    var role = await _userRoleRepository.GetByIdAsync(userResponse.RoleId, cancellationToken);
                    userResponse.Role = role.Role;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("user/get-all", ex));
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "GAME MASTER")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id, cancellationToken);
                if (user is null) return NotFound(ErrorResponseModel.NewError("user/get-one", "user not found"));

                var userRole = await _userRoleRepository.GetByIdAsync(user.RoleId, cancellationToken);
                var responseRepo = await _userRepository.GetByIdAsync(id, cancellationToken);
                var response = _mapper.Map<UserModel>(responseRepo);
                response.Role = userRole.Role;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("user/get-one", ex));
            }
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile(CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                if (user is null) return NotFound(ErrorResponseModel.NewError("user/profile", "user not found"));

                var userRole = await _userRoleRepository.GetByIdAsync(user.RoleId, cancellationToken);
                var responseRepo = await _userRepository.GetByIdAsync(User.GetSubjectId(), cancellationToken);
                var response = _mapper.Map<UserModel>(responseRepo);
                response.Role = userRole.Role;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("user/profile", ex));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(InsertUserRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var checkEmail = await _userRepository.CheckEmailAsync(request.Email, cancellationToken);
                if (checkEmail) return BadRequest(ErrorResponseModel.NewError("user/insert", "email already exists"));
                var roles = await _userRoleRepository.GetAsync(cancellationToken);
                var newUser = new UserModel
                {
                    CharacterId = request.CharacterId,
                    Name = request.Name,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    RoleId = roles.Where(c => c.Role == "PLAYER").FirstOrDefault()!.Id
                };
                var mapUser = _mapper.Map<UserBson>(newUser);
                await _userRepository.InsertAsync(mapUser, cancellationToken);
                return Ok();
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
                if (findUser.Email != request.Email)
                {
                    var checkEmail = await _userRepository.CheckEmailAsync(request.Email, cancellationToken);
                    if (checkEmail) return BadRequest(ErrorResponseModel.NewError("user/update", "email already exists"));
                }
                var updateUser = new UserModel
                {
                    Id = request.Id,
                    CharacterId = request.CharacterId,
                    Name = request.Name,
                    Email = request.Email,
                };
                var mapUser = _mapper.Map<UserBson>(updateUser);
                await _userRepository.UpdateAsync(mapUser, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("user/update", ex));
            }
        }
        [HttpPatch("{id}/changepassword")]
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
                await _userRepository.ChangePasswordAsync(id, BCrypt.Net.BCrypt.HashPassword(request.NewPassword), cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponseModel.NewError("user/change-password", ex));
            }
        }
    }
}
