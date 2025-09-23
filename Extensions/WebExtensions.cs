using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PolyhydraGames.Extensions;

public static class WebExtensions
{
    public static async Task<string> GetStringAsync(this HttpClient client, string url)
    {
        return await client.GetStringAsync(new Uri(url));
    }
    public static Dictionary<string, string> ToDictionary(this string query)
    {
        var plainText = UnQuery(query);
        var dict = plainText.Split('&').Select(x => x.Split('=')).ToDictionary(x => x[0], x => x[1]);
        return dict;
    }
    public static string NormalizeQuery(this string uri)
    {
        return uri.Replace(" ", "%20").Replace(":", "%3A");
    }
    public static string UnQuery(this string uri)
    {
        return uri.Replace("%20", " ").Replace("%3A", ":");
    }
    public static string StripHtmlRoot(this string uri)
    {
        return uri.Between("://", "/");
    }


}