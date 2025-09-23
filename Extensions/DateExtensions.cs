using System;

namespace PolyhydraGames.Extensions;

public static class DateExtensions
{
    public static bool IsTimeGreaterThanOrEqual(this DateTime first, DateTime second)
    { 
        if (first.DayOfYear == second.DayOfYear && first.Hour == second.Hour &&
            first.Minute == second.Minute) return true;

        if (first.DayOfYear < second.DayOfYear) return false;
        if (first.DayOfYear > second.DayOfYear) return true;

        if (first.Hour > second.Hour) return true;
        if (first.Hour < second.Hour) return false;

        return first.Minute > second.Minute;
    }
    public static bool IsTimeGreaterThan(this DateTime first, DateTime second)
    {

        if (first.DayOfYear > second.DayOfYear) return true;
        if (first.DayOfYear < second.DayOfYear) return false;

        if (first.Hour > second.Hour) return true;
        if (first.Hour < second.Hour) return false;

        return first.Minute > second.Minute;
    }
}