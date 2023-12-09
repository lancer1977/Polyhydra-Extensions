using System;
using System.Security.Cryptography;
using System.Text;

namespace PolyhydraGames.Extensions
{
    public static class CryptoExtensions
    {
        public static byte[] Sha256(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            }

        }

        public static string Base64UrlEncode(byte[] data)
        {
            return Convert.ToBase64String(data)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }


        public static string CreateUniqueId(int length)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[length];
                rng.GetBytes(bytes);
                return Base64UrlEncode(bytes);
            };

        }


    }
}