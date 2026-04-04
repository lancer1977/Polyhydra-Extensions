using System;

namespace PolyhydraGames.Extensions;

public static class DateExtensions
{
    public static bool IsTimeGreaterThanOrEqual(this DateTime first, DateTime second)
    {
        return first >= second;
    }
    public static bool IsTimeGreaterThan(this DateTime first, DateTime second)
    {
        return first > second;
    }
}
