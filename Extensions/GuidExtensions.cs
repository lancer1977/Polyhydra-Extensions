using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PolyhydraGames.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsGuid(this string value)
        {
            return Guid.TryParse(value, out var _);
        }

        public static bool IsEmpty(this Guid input)
        {
            return input == Guid.Empty;
        }
        public static Guid ToGuid(this string value)
        {
            if (string.IsNullOrEmpty(value) || !Guid.TryParse(value, out var id)) return Guid.Empty;
            Debug.WriteLine(id);
            return id;

        }
    }
}