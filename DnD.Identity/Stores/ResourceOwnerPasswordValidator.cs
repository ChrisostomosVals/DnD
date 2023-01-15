using DnD.Data.Repositories;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace DnD.Identity.Stores
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ILogger<ResourceOwnerPasswordValidator> _logger;
        private readonly UserRepository _userRepository;
        private readonly UserRoleRepository _userRoleRepository;

        public ResourceOwnerPasswordValidator(UserRepository userRepository, ILogger<ResourceOwnerPasswordValidator> logger, UserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _userRoleRepository = userRoleRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(context.UserName);
                if (user is null)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, errorDescription: "Invalid Username or Password");
                    context.Result.IsError = true;
                    return;
                }
                var isVerified = BCrypt.Net.BCrypt.Verify(context.Password, user.PASSWORD);
                if (isVerified)
                {
                    var userRole = await _userRoleRepository.GetAsync(user.ROLE_ID);
                    context.Result = new GrantValidationResult(user.ID,
                        authenticationMethod: AccessTokenType.Jwt.ToString(),
                        claims: new Collection<Claim> { new Claim("role", userRole.ROLE) }, identityProvider: "DnD Renia");
                    context.Result.IsError = false;
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, errorDescription: "Invalid Username or Password");
                    context.Result.IsError = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "[ROPV] An error occured in resource validation.");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, errorDescription: ex.Message);
            }
        }
    }
}
       
