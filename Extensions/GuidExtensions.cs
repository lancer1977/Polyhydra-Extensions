using System;

namespace PolyhydraGames.Extensions;

public static class GuidExtensions
{
    public static bool IsNew(this Guid guid) => guid == Guid.Empty;

    public static bool IsNew(this Guid? guid) => guid == null || guid == Guid.Empty;

    public static bool IsGuid(this string value) => Guid.TryParse(value, out _);

    public static bool IsEmpty(this Guid input) => input == Guid.Empty;

    public static Guid ToGuid(this string value)
    {
        if (string.IsNullOrEmpty(value) || !Guid.TryParse(value, out var id))
            return Guid.Empty;
        return id;
    }
}