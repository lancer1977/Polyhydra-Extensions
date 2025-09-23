using System;
using System.Security.Claims;

namespace PolyhydraGames.Extensions;

public static class ClaimsPrincipalExtensions
{

    public static string GetEmail(this ClaimsPrincipal principal)
    {
        return principal.Claims.GetEmail();
    }

    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        return principal.Claims.GetUserId();
    }

    public static string GetUserName(this ClaimsPrincipal principal)
    {
        return principal.Claims.GetUserName();
    }
    public static bool IsExpired(this ClaimsPrincipal principal)
    {
        return principal.Claims.IsExpired();
    }
}