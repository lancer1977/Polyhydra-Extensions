using System;

namespace PolyhydraGames.Extensions
{
    public static class HtmlStyleHelpers
    {
        public static string Show(bool b) => b ? "" : "display:none;";
    }
    public static class HttpEncoding
    {
        public static string ToUrlEncoded(this string value)
        {
            return Uri.EscapeDataString(value);

        }
    }
}

