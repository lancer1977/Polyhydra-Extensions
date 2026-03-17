using System.IO;

namespace PolyhydraGames.Extensions;

public static class StreamHelper
{
    public static Stream ToStream(this string str)
    {
        var stream = new MemoryStream();
        using var writer = new StreamWriter(stream, leaveOpen: true);
        writer.Write(str);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}