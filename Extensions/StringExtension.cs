using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PolyhydraGames.Extensions;

public static class StringExtension
{
    public static bool IsNullOrEmpty(this string? value, bool includeWhitespace = false)
    {
        return includeWhitespace ? string.IsNullOrWhiteSpace(value) : string.IsNullOrEmpty(value);
    }

    public static bool IsUrl(this string request)
    {
        if (string.IsNullOrWhiteSpace(request))
        {
            return false;
        }

        var value = request.Trim();

        // Keep scheme-based URLs permissive anywhere in the string.
        if (Regex.IsMatch(value, @"(?i)(?:https?|ftp)://\s*\S+"))
        {
            return true;
        }

        // Allow bare domains when the entire value is the URL.
        if (Regex.IsMatch(value, @"(?i)^(?:[a-z0-9-]+\.)+[a-z]{2,}(?:/\S*)?$"))
        {
            return true;
        }

        // Allow bare domains embedded in text only when they include a path.
        return Regex.IsMatch(value, @"(?i)\b(?:[a-z0-9-]+\.)+[a-z]{2,}/\S+");
    }

    public static string Truncate(this string? value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || maxLength <= 0)
            return string.Empty;

        if (value.Length <= maxLength)
            return value;

        const string ellipsis = "...";
        if (maxLength <= ellipsis.Length)
            return value[..maxLength];

        return value[..(maxLength - ellipsis.Length)] + ellipsis;
    }


    public static string ToTitleCase(this string input)
    {

        var words = input.Split(' ');
        var result = new List<string>();
        foreach (var word in words)
        {
            if (word.Length == 0 || AllCapitals(word))
                result.Add(word);
            else if (word.Length == 1)
                result.Add(word.ToUpper());
            else
                result.Add(char.ToUpper(word[0]) + word.Remove(0, 1).ToLower());
        }

        return string.Join(" ", result);
    }

    public static bool AllCapitals(string input)
    {
        return input.ToCharArray().All(char.IsUpper);
    }
    public static string TextAfterFirstInstanceOfString(this string value, string test)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(test))
            return "";
        var num = value.IndexOf(test, StringComparison.Ordinal);
        if (num < 0)
            return "";
        return value.Substring(num + test.Length);
    }

    public static string TextBeforeFirstInstanceOfString(this string value, string test)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(test))
            return "";
        var length = value.IndexOf(test, StringComparison.Ordinal);
        if (length <= 0)
            return "";
        return value.Substring(0, length);
    }

    public static string ValueAfter(this string baseString, string identifier, string betweenStart, string betweenEnd)
    {
        var searchString = baseString.TextAfterFirstInstanceOfString(identifier);
        var next = searchString.TextAfterFirstInstanceOfString(betweenStart);
        return next.TextBeforeFirstInstanceOfString(betweenEnd);
    }
}
