using System;
using System.Diagnostics;

namespace PolyhydraGames.Extensions
{
    public static class GuidExtensions
    {
        public static Guid ToGuid(this string value)
        {
            if (string.IsNullOrEmpty(value) || !Guid.TryParse(value, out var id)) return Guid.Empty;
            Debug.WriteLine(id);
            return id;

        }
    }
}