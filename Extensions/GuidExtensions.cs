using System;
using System.Diagnostics;

namespace PolyhydraGames.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsNew(this Guid guid)
        {
            return guid == Guid.Empty;
        }
        public static bool IsNew(this Guid? guid)
        {
            return guid == null || guid == Guid.Empty;
        }
        
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