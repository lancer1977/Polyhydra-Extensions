using System.Net;
using System.Text;

namespace PolyhydraGames.Extensions;

public static class HttpListenerExtensions
{
    public static void WriteJson(this HttpListenerContext context, string json)
    {
        context.AddCorsHeaders();
        var buffer = Encoding.UTF8.GetBytes(json);
        context.Response.ContentType = "application/json";
        context.Response.ContentLength64 = buffer.Length;
        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        context.Response.Close();
    }

    public static void WriteHtml(this HttpListenerContext context, string html)
    {
        context.AddCorsHeaders();
        var buffer = Encoding.UTF8.GetBytes(html);
        context.Response.ContentType = "text/html";
        context.Response.ContentLength64 = buffer.Length;
        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        context.Response.Close();
    }

    public static void AddCorsHeaders(this HttpListenerContext context)
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    }
}