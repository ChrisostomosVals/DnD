using System.Security.Claims;

namespace DnD.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetSubjectId(this ClaimsPrincipal user)
        {
            if (user == null)
                return null;

            if (!user.Identity.IsAuthenticated)
                return null;

            if (user.HasClaim(c => c.Type == "sub"))
            {
                return user.FindFirstValue("sub");
            }
            else if (user.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                return user.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return null;
        }
    }
}
