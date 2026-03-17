using System;
using System.Security.Cryptography;
using System.Text;

namespace PolyhydraGames.Extensions;

public static class CryptoExtensions
{
    /// <summary>
    /// Computes the SHA256 hash for the given input string.
    /// </summary>
    public static byte[] Sha256(string input)
    {
        using var sha256 = SHA256.Create();
        return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
    }

    /// <summary>
    /// Encodes the given byte array into a Base64Url string.
    /// </summary>
    public static string Base64UrlEncode(byte[] data) =>
        Convert.ToBase64String(data)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');

    /// <summary>
    /// Creates a unique ID of the specified length.
    /// </summary>
    public static string CreateUniqueId(int length)
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        return Base64UrlEncode(bytes);
    }
}