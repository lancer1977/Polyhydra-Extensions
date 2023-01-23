using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace PolyhydraGames.Extensions
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static partial class Text
    {
    

        public static string ToNumericWordAbreviation(this int value)
        {
            switch (value)
            {
                case 1:
                    return "1st";
                case 2:
                    return "2nd";
                case 3:
                    return "3rd";
                default:
                    return value + "th";
            }
        }

        public static string FirstInt(this string value)
        {
            var output = "";
            var foundInt = false;
            for (var x = 0; x < value.Length; x++)
            {
                int outint;
                if (value[x].IsNumeric(out outint))
                {
                    output += outint.ToString();
                    foundInt = true;
                }
                else
                {
                    if (foundInt)
                        break;
                }
            }
            return output;
        }

        public static string FirstWord(this string value)
        {
            var output = "";
            for (var x = 0; x < value.Length; x++)
            {
                if (value[x].IsNumeric())
                    break;
                output += value[x];
            }
            return output;
        }

        public static IEnumerable<string> FirstLetters<T>(this IEnumerable<T> items)
        {
            return (from item in items select item.ToString()[0].ToString()).ToArray();
        }

        public static string TextAfterString(this string value, string test)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(test)) return "";
            var index = value.LastIndexOf(test, StringComparison.Ordinal);
            return index > 0 ? value.Substring(index + test.Length) : "";
        }

        public static string TextBeforeString(this string value, string test)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(test)) return "";
            var index = value.LastIndexOf(test, StringComparison.Ordinal);
            return index > 0 ? value.Substring(0, index) : "";
        }

        //public static string ToJson<T>(this T item)
        //{
        //    string myval = JsonConvert.SerializeObject(item);
        //    return myval;
        //}

        //public static T FromJson<T>(this string code) where T : new()
        //{
        //    var item = JsonConvert.DeserializeObject<T>(code);
        //    return item;
        //}

        public static string RemoveIllegalCharacters(this string aString)
        {
            if (string.IsNullOrEmpty(aString)) return aString;
            aString = aString.Replace(Environment.NewLine, "(VBNEWLINE)");
            aString = aString.Replace("\"", "(QUOTES)");
            aString = aString.Replace("@", "(ANDSYMBOL)");
            aString = aString.Replace("#", "(POUNDSYMBOL)");
            aString = aString.Replace(":", "(COLINSYMBOL)");
            return aString;
        }

        public static string Between(this string aString, string character1, string character2, bool between)
        {
            var index1 = string.IsNullOrEmpty(character1) ? 0 : aString.IndexOf(character1, StringComparison.Ordinal);
            if (character2 == null)
                return aString.Substring(index1);
            var length = character1 != character2
                ? (aString.IndexOf(character2, StringComparison.Ordinal) + 1 - index1)
                : aString.LastIndexOf(character2, StringComparison.Ordinal) + 1 - index1;
            if (index1 >= 0 && length > 0)
                return aString.Substring(index1, length);
            return "";
        }

        public static string Between(this string aString, string character1, string character2)
        {
            var index1 = string.IsNullOrEmpty(character1)
                ? 0
                : aString.IndexOf(character1, StringComparison.Ordinal) + 1;
            if (character2 == null)
                return aString.Substring(index1);
            var length = character1 != character2
                ? (aString.IndexOf(character2, StringComparison.Ordinal) - index1)
                : aString.LastIndexOf(character2, StringComparison.Ordinal) - index1;
            if (index1 >= 0 && length > 0)
                return aString.Substring(index1, length);
            return "";
        }

        public static string RestoreIllegalCharacters(this string aString)
        {
            aString = aString.Replace("(QUOTES)", "\"");
            aString = aString.Replace("(VBNEWLINE)", Environment.NewLine);
            aString = aString.Replace("(ANDSYMBOL)", "@");
            aString = aString.Replace("(POUNDSYMBOL)", "#");
            aString = aString.Replace("(COLINSYMBOL)", ":");
            return aString;
        }

        public static string[] FromCodedArray(this string code, char split = '#')
        {
            return code.Split(split);
        }

        public static string ToCodedArray<T>(this IEnumerable<T> aryArray, string store = "#")
        {
            if (aryArray == null) return "";
            var items = aryArray.ToArray();
            return (items.Any())
                ? (from object itemLoopVariable in items select itemLoopVariable.ToString()).Aggregate(
                    (current, item) => current + store + item.ToString())
                : "";
        }

        public static string ToCodedArrayBuilder<T>(this IEnumerable<T> aryArray, string store = "#")
        {
            if (aryArray == null) return "";
            var enumerable = aryArray as T[] ?? aryArray.ToArray();
            var builder = enumerable.Aggregate(
                new StringBuilder(),
                (sb, s) => sb.AppendLine(s.ToString() + store)
                );
            var final = builder.ToString();
            var length = final.Length - store.Length;
            return final.Substring(0, length);
        }

        public static string ToDictionaryCodedString<T, TE>(this Dictionary<T, TE> aryArray, string mergevalue = ":", string store = "#")
        {
            return aryArray == null
                ? ""
                : aryArray.Select(item => item.Key + mergevalue + item.Value).ToCodedArray(store);
        }

        public static string[] ToDictionaryCodedArray<T, TE>(this Dictionary<T, TE> aryArray, string mergevalue = ":", string store = "#")
        {
            return aryArray == null
                ? new string[0]
                : aryArray.Select(item => item.Key + mergevalue + item.Value).ToArray();
        }

        public static string ToCodedArrayWithSpace<T>(this IEnumerable<T> aryArray, string store = "#")
        {
            if (aryArray == null) return "";
            var items = aryArray.ToArray();
            return (items.Length > 0)
                ? (from object itemLoopVariable in items select itemLoopVariable.ToString()).Aggregate(
                    (current, item) => current + store + item.ToString().InsSpace())
                : "";
        }

        public static string InsSpace(this string aString)
        {
            return !(aString.Length > 2) ? aString : aString[0] + Regex.Replace(aString.Substring(1), "[A-Z]", " $&");
        }

        public static string InsSpaceBeforeInt(this string aString)
        {
            if (string.IsNullOrEmpty(aString)) return "";
            if (!(aString.Length > 2)) return aString;
            var newString = "";
            for (var x = 0; x <= aString.Length; x++)
            {
                newString += (char.IsLower(aString[x]) && char.IsNumber(aString[x + 1]))
                    ? aString[x] + " "
                    : aString[x].ToString();
            }
            newString += aString[aString.Length - 1];
            return newString;
        }

        public static string VbMe(this string input)
        {
            return input + Environment.NewLine;
        }

        public static string ToLettersNumbersUnderscoreNoDash(this string value)
        {
            return Regex.Replace(value, @"[^A-Za-z0-9_]", "");
        }

        public static string ToProperCase(this string value)
        {
            if (value == null)
                return null;
            if (value.Length == 0)
                return value;
            var result = new StringBuilder(value);
            result[0] = char.ToUpper(result[0]);
            for (var i = 1; i < result.Length; ++i)
            {
                if (char.IsWhiteSpace(result[i - 1]))
                    result[i] = char.ToUpper(result[i]);
                else
                    result[i] = char.ToLower(result[i]);
            }
            return result.ToString();
        }

        public static string Capitalize(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var letter1 = char.ToUpperInvariant(value[0]);
            return value.Length >= 2 ? letter1 + value.Substring(1) : letter1.ToString();
        }

        public static string ToCapitalizedWords(this string value)
        {
            var items = value.Split(' ');
            if (items.Length == 1) return items[0].Capitalize();
            string[] returnList = items.Select(item => item.Capitalize()).ToArray();
            return returnList.ToCodedArray(" ");
        }

        public static string ToNumbers(this string value)
        {
            return Regex.Replace(value, @"[^0-9\-]", "");
        }

        public static string[] ToStringArray<T>(this IEnumerable<T> aryArray)
        {
            var returnMe = new List<string>();
            if (aryArray == null) return returnMe.ToArray();
            returnMe.AddRange(aryArray.Cast<object>().Select(itemLoopVariable => itemLoopVariable.ToString()));
            return returnMe.ToArray();
        }

        public static bool IsNumeric(this char value)
        {
            int outnum;
            return value.IsNumeric(out outnum);
        }
 
        public static string StringFromCodedArray(this string code, string replaceWith, char split = '#')
        {
            return code.Replace(split.ToString(), replaceWith);
        }

        public static string ReplaceAllBetween(this string aString, string character1, string character2, bool characters)
        {
            if (aString.Contains(character1) && aString.Contains(character2))
            {
                aString = aString.Replace(aString.Between(character1, character2, characters), "");
                aString = aString.ReplaceAllBetween(character1, character2, characters);
            }
            return aString;
        }

        public static string ToLettersNumbersUnderscoreSpace(this string value)
        {
            return Regex.Replace(value, @"[^A-Za-z0-9_ ]", "");
        }

        public static string ToEnumNormalized(this string value)
        {
            return Regex.Replace(value, @"[^A-Za-z0-9_]", "");
        }

        public static string ToLettersNumbersUnderscore(this string value)
        {
            return Regex.Replace(value, @"[^A-Za-z0-9_\-]", "");
        }

        public static string ToAlphaNumeric(this string value)
        {
            return Regex.Replace(value, @"[^A-Za-z0-9\- ]", "");
        }

        public static string[] SplitText(this string code)
        {
            var newline = Environment.NewLine;
            if (newline == "\r\n")
            {
                code = code.Replace(Environment.NewLine, "\r");
                code = code.Replace("\n", "");
                return code.Split('\r');
            }
            return code.Split('\n');
        }

        public static string[] ToStringArrayFromEnum<T>(this IEnumerable<T> aryArray)
        {
            var returnMe = new List<string>();
            if (aryArray == null) return returnMe.ToArray();
            returnMe.AddRange(aryArray.Cast<object>().Select(itemLoopVariable => itemLoopVariable.ToString().InsSpace()));
            return returnMe.ToArray();
        }
    }
}