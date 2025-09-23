using System;

namespace PolyhydraGames.Extensions;

public static class FormattingExtensions
{
    public static string MSToTime(this int? ms)
    {
        return ms.HasValue ? ms.Value.MSToTime() : "00:00";
    }

    public static string MSToTime(this int ms)
    {
        return TimeSpan.FromMilliseconds(ms).ToString(@"mm\:ss");
    }
}