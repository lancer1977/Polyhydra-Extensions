using System.IO;
using System.Reflection;

namespace PolyhydraGames.Extensions;

public static class AssemblyFileReader
{
    public static string GetEmbeddedText(Type sharedType, string name)
    {
        var assembly = sharedType.GetTypeInfo().Assembly;
        using var stream = assembly.GetManifestResourceStream(name);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public static string GetEmbeddedResource(Type sharedType, string name, string localPath)
    {
        var assembly = sharedType.GetAssembly();
        var resourceName = $"{assembly.GetName().Name}.{localPath}.{name}".Replace(@"\", ".");
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public static TextReader GetTextReader(Type sharedType, string name)
    {
        var assembly = sharedType.GetAssembly();
        var stream = assembly.GetManifestResourceStream(name);
        return new StreamReader(stream);
    }
}