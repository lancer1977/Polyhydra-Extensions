using System;
using System.IO; 

namespace PolyhydraGames.Extensions;

public static class FileExtensionHelpers
{
    static string[] audioExtensions = { ".mp3", ".wav", ".ogg", ".flac", ".aac", ".wma" /* Add more as needed */ };
    static string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" /* Add more as needed */ };
    public static bool IsImageFile(this string file)
    {
        var extension = Path.GetExtension(file);
        return Array.Exists(imageExtensions, ext => ext == extension);
    }
    public static bool IsAudioFile(this string file)
    {
        var extension = Path.GetExtension(file);
        return Array.Exists(audioExtensions, ext => ext == extension);
    }
    public static bool MatchesPattern(string filename, string pattern)
    {
        return Path.GetFileName(filename).Contains(pattern);
    }


}