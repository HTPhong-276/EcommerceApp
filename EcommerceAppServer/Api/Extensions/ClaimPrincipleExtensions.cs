using System.Security.Claims;

namespace Api.Extensions
{
    public static class ClaimPrincipleExtensions
    {
        public static string RetriveEmailFromPrinciple(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}
