using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyhydraGames.Extensions;

public static class StringExtension
{


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