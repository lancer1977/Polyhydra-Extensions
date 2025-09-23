using System;
using System.Security.Cryptography;
using System.Text;

namespace PolyhydraGames.Extensions;

public static class CryptoExtensions
{
    /// <summary>
    /// Computes the SHA256 hash for the given input string.
    /// </summary>
    /// <param name="input">The input string to compute the hash for.</param>
    /// <returns>The computed SHA256 hash as a byte array.</returns>
    public static byte[] Sha256(string input)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

    }

    /// <summary>
    /// Encodes the given byte array into a Base64Url string.
    /// </summary>
    /// <param name="data">The byte array to encode.</param>
    /// <returns>The encoded string.</returns>
    public static string Base64UrlEncode(byte[] data)
    {
        return Convert.ToBase64String(data)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }

    /// <summary>
    /// Creates a unique ID of the specified length.
    /// </summary>
    /// <param name="length">The length of the unique ID to create.</param>
    /// <returns>The created unique ID.</returns>
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