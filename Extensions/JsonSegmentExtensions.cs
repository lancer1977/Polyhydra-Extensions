using System.Collections.Generic;
using System.Text.Json;

namespace PolyhydraGames.Extensions;

public static class JsonSegmentExtensions
{
    public static IEnumerable<T>? GetJsonSegment<T>(this string json, string segment)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (root.TryGetProperty(segment, out var itemsElement))
        {
            var elements = itemsElement.GetRawText();
            return JsonSerializer.Deserialize<List<T>>(elements);
        }

        return null;
    }
}