using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PolyhydraGames.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetEmail(this IEnumerable<Claim> claims)
        {
            return claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        }

        public static Guid GetUserId(this IEnumerable<Claim> claims)
        {
            return claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value.ToGuid() ?? Guid.Empty;
        }

        public static string GetUserName(this IEnumerable<Claim> claims)
        {
            return claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "";
        }

        public static bool IsExpired(this IEnumerable<Claim> claims)
        {
            var exp = claims?.First(claim => claim.Type == ClaimTypes.Expiration).Value;
            var expirationDate = DateTime.Parse(exp);
            return expirationDate < DateTime.UtcNow;
        }
    }
}