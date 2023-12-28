using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyhydraGames.Extensions
{
    public static class Exts
    {
        public static void ForEachItem(this IEnumerable<string> items, Action<string> act)
        {
            foreach (var item in items)
            {
                act(item);
            }
        }

        public static List<string> SplitAndList(this string aString, char split = '#')
        {
            return aString.Split(split).ToList();
        }

        public static bool IsNumeric(this string value, out int result) // FAILS IF ANY LETTERS EXIST
        {
            return int.TryParse(value, out result);
        }

        public static bool IsNumeric(this char value, out int result)
        {
            return value.ToString().IsNumeric(out result);
        }


        public static bool Between(this int value, int start, int end)
        {
            return (value >= start) && (value <= end);
        }

        public static int ToIntOrNegative1(this object value)
        {
            return string.IsNullOrEmpty(value.ToString()) ? -1 : Convert.ToInt32(value);
        }

        public static int ToInt(this object value)
        {
            return string.IsNullOrEmpty(value.ToString()) ? 0 : Convert.ToInt32(value);
        }

        public static int ToInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            value = value.ToNumbers();
            int x;
            int.TryParse(value, out x);
            return x;
        }

        public static bool ToBool(this object value)
        {
            return value.ToString().ToBool();
        }

        public static bool ToBool(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            int number;
            return value.IsNumeric(out number) ? number.ToBool() : bool.Parse(value);
        }

        public static bool ToBool(this int value)   
        {
            return value > 0;
        }
    }
}