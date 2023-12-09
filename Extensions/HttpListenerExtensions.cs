using System.Net;

namespace PolyhydraGames.Extensions
{
    public static class HttpListenerExtensions
    {
        public static void WriteJson(this HttpListenerContext context, string html)
        {
            context.AddCorsheaders();
            var buffer = System.Text.Encoding.UTF8.GetBytes(html);
            context.Response.ContentType = "text/json";
            context.Response.ContentLength64 = buffer.Length;
            // Write the main response content
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            // Close the response
            context.Response.Close();
        }

        public static void WriteHtml(this HttpListenerContext context, string html)
        {
            context.AddCorsheaders();
            var buffer = System.Text.Encoding.UTF8.GetBytes(html);
            context.Response.ContentType = "text/html";
            context.Response.ContentLength64 = buffer.Length;

            // Write the main response content
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);

            // Close the response
            context.Response.Close();
        }

        public static void AddCorsheaders(this HttpListenerContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

        }
    }
}