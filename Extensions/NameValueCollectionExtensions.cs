using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace PolyhydraGames.Extensions;

public static class NameValueCollectionExtensions
{
    public static string ToQueryString(this NameValueCollection collection)
    {
        var array = (from key in collection.AllKeys
                from value in collection.GetValues((string)key)
                select $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}")
            .ToArray();
        return "?" + string.Join("&", array);
    }

    public static Dictionary<string, string> ToDictionary(this NameValueCollection collection)
    {
        return collection.AllKeys.ToDictionary(key => key, key => collection[key]);
    }

}