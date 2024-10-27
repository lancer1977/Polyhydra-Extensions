using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace PolyhydraGames.Extensions
{
    public static class JsonSegmentExtensions
    {
        public static IEnumerable<T> GetJsonSegment<T>(this string json, string segment)
        {
            using var document = JsonDocument.Parse(json);
            // Access the root JSON object
            var root = document.RootElement;

            // Check if the root has the "items" property
            if (root.TryGetProperty(segment, out var itemsElement))
            {
                // Deserialize the "items" portion using your specific item class
                // Replace YourItemClass with the actual class representing the items
                var elements = itemsElement.GetRawText();
                Debug.WriteLine(elements);
                var items = JsonSerializer.Deserialize<List<T>>(elements); 
                return items;
            }

            return null;
        }
    }
}