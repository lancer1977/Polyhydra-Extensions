using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyhydraGames.Extensions
{

    public static class EnumExtensions
    {
        public static T ToEnum<T>(this Enum value)
        {
            var converted = value.ToString();
            return Enum.IsDefined(typeof(T), converted)
                       ? (T)Enum.ToObject(typeof(T), value)
                       : (T)Enum.ToObject(typeof(T), 0);
        }
        public static T ToEnum<T>(this int value)
        {
            return (T)Enum.ToObject(typeof(T), value);
            //: (T)Enum.ToObject(typeof(T), 0);
        }
        public static T ToEnum<T>(this string value) where T : struct
        {
            var outVal = default(T);
            if (string.IsNullOrEmpty(value)) return outVal;

            if (Enum.TryParse(value, true, out outVal))
            {
                return outVal;
            }

            Enum.TryParse(value.ToEnumNormalized(), true, out outVal);
            return outVal;
        }
        public static bool EnumStringCompare<T>(T enumItem, string value)
        {
            var normalized2 = enumItem.ToString().ToEnumNormalized();
            var normalized1 = value.ToEnumNormalized();
            return string.Equals(normalized2, normalized1, StringComparison.OrdinalIgnoreCase);

        }

        public static List<T> ToEnum<T>(this string[] value) where T : struct
        {
            return ToIEnum<T>(value).ToList();
        }
        public static IEnumerable<T> ToIEnum<T>(this string[] value) where T : struct
        {
            var newlist = new List<T>();
            if (value != null && value.Any())
            {
                newlist.AddRange(value.Select(itemLoopVariable => itemLoopVariable.ToEnum<T>()));
            }
            return newlist;
        }
        public static IEnumerable<T> ToEnum<T>(this IEnumerable<string> value) where T : struct
        {
            return value.Select(itemLoopVariable => itemLoopVariable.ToEnum<T>()).ToList();
        }
        public static IEnumerable<T> ToEnum<T>(this IEnumerable<int> value)
        {
            return value.Select(itemLoopVariable => itemLoopVariable.ToEnum<T>()).ToList();
        }
        public static IEnumerable<T> ToEnumList<T>(this string value, char code = '#') where T : struct
        {
            return value.Split(code).ToEnum<T>();
        }

        public static string[] EnumToArray<T>(this IEnumerable<T> sAlign, bool insertSpaces = true) where T : struct
        {
            var outList = (from item in sAlign select (insertSpaces ? item.ToString().InsSpace() : item.ToString())).ToArray();
            return outList;
        }
        public static List<string> EnumToList<T>(this IEnumerable<T> sAlign, bool insertSpaces = true) where T : struct
        {
            var outList = (from item in sAlign
                           select (insertSpaces ? item.ToString().InsSpace() : item.ToString())).ToList();
            return outList;
        }
        public static List<string> EnumTypeToList<T>(bool insertSpace = true)
        {

            return EnumTypeToArray<T>(insertSpace).ToList();
        }
        public static string[] EnumTypeToArray<T>(bool insertSpace = true)
        {
            var enumType = typeof(T);
            var enumValArray = Enum.GetValues(enumType);
            var enumList = new List<string>(enumValArray.Length);
            foreach (var itemLoopVariable in enumValArray)
            {
                var item = itemLoopVariable;
                enumList.Add(insertSpace ? item.ToString().InsSpace() : item.ToString());
            }
            return enumList.ToArray();
        }

        public static T[] EnumerateEnumType<T>()
        {
            var enumType = typeof(T);
            var enumValArray = Enum.GetValues(enumType);
            return (from object itemLoopVariable in enumValArray select (T)itemLoopVariable).ToArray();
        }


        public static T[] FromIntArray<T>(this int[] value)
        {
            var result = new T[value.Length];
            for (var i = 0; i < value.Length; i++)
                result[i] = (T)Enum.ToObject(typeof(T), value[i]);
            return result;
        }

        public static int[] ToIntArray<T>(this T[] value)
        {
            var result = new int[value.Length];
            for (var i = 0; i < value.Length; i++)
                result[i] = Convert.ToInt32(value[i]);
            return result;
        }

        public static T[] EnumOptionArray<T>()
        {
            var enumType = typeof(T);
            var enumValArray = Enum.GetValues(enumType);
            var enumList = new List<T>(enumValArray.Length);
            foreach (var itemLoopVariable in enumValArray) // Dont REfactor
            {
                var item = itemLoopVariable;
                enumList.Add(item.ToInt().ToEnum<T>());
            }
            return enumList.ToArray();
        }


    }
}