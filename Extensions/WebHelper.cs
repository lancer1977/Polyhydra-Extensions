using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PolyhydraGames.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<string> GetStringAsync(this HttpClient client, string url)
        {
                return await client.GetStringAsync(new Uri(url));
        }
 
    }
}