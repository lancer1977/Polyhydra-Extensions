using System;

namespace PolyhydraGames.Extensions;

public static class HttpEncoding
{
    public static string ToUrlEncoded(this string value)
    {
        return Uri.EscapeDataString(value);

    }
}