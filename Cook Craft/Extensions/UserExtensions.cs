using System.Security.Claims;

namespace Cook_Craft.Extensions;

public static class UserExtensions
{
    public static string GetUserId(this ClaimsPrincipal user) => user.FindFirst(ClaimTypes.NameIdentifier).Value;
}