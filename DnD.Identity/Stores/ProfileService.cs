using DnD.Data.Repositories;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace DnD.Identity.Stores
{
    public class ProfileService : IProfileService
    {
        private readonly ILogger<ProfileService> _logger;
        private readonly UserRepository _userRepository;
        private readonly UserRoleRepository _userRoleRepository;
        public ProfileService(ILogger<ProfileService> logger, UserRepository userRepository, UserRoleRepository userRoleRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subjectClaim = context.Subject.FindFirst(JwtClaimTypes.Subject);
                if (subjectClaim != null)
                {
                    var user = await _userRepository.GetByIdAsync(subjectClaim.Value);
                    var userRole = await _userRoleRepository.GetByIdAsync(user.RoleId);
                    context.IssuedClaims.AddRange(new Collection<Claim> { new Claim("role", userRole.Role) });
                    if (user.Name is not null)
                    {
                        context.IssuedClaims.Add(new Claim(JwtClaimTypes.PreferredUserName, user.Name));
                    }
                    context.IssuedClaims.Add(new Claim(JwtClaimTypes.Email, user.Email));
                    context.IssuedClaims.Add(new Claim(JwtClaimTypes.EmailVerified, "true"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("[ProfileService] There was an error on getting user profile data message: {0}", ex.Message);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subjectClaim = context.Subject.FindFirst(JwtClaimTypes.Subject);
            if (subjectClaim != null)
            {
                var user = await _userRepository.GetByIdAsync(subjectClaim.Value);
                if (user is null)
                    context.IsActive = false;
                else
                    context.IsActive = true;
            }
            else
            {
                context.IsActive = false;
            }
        }
    }
}
