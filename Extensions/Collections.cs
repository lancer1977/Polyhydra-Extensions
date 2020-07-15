using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PolyhydraGames.Extensions.Dice;

namespace PolyhydraGames.Extensions
{
    public static class Collections
    {
        public static bool IsAny<T>(this T item, params T[] options)
        {
            return options.Contains(item);
        }

        public static bool ContainsAny<T>(this IEnumerable<T> set1, IEnumerable<T> set2)
        {
            if (set2 == null)
                return false;
            var set1Array = set1.ToArray();
            foreach (var item in set2)
            {
                if (set1Array.Contains(item))
                    return true;
            }
            return false;
        }
        public static IEnumerable<T> Union<T>(this IEnumerable<T> set1, IEnumerable<T> set2)
        {
            var items = set1.ToList();
            foreach (var item in set2.Where(item => !items.Contains(item)))
                items.Add(item);
            return items;
        }

        public static IEnumerable<T> Intersection<T>(this IEnumerable<T> set1, IEnumerable<T> set2)
        {
            return set2.Where(set1.Contains).ToList();
        }

        public static IEnumerable<T> SymetricDifference<T>(this IEnumerable<T> set1, IEnumerable<T> set2)
        {
            var set1array = set1.ToArray();
            var set2array = set2.ToArray();
            var union = Union(set1array, set2array);
            var intersection = Intersection(set1array, set2array);
            return union.SetComplement(intersection.ToArray());
        }
        public static string[] Add(this string[] array1, string array2)
        {
            array1 = array1.Concat(new[] { array2 }).ToArray();
            return array1;
        }
        public static string[] AddRange(this string[] array1, string[] array2)
        {
            array1 = array1.Concat(array2).ToArray();
            return array1;
        }
        public static int[] MergeArray(this int[] array1, int array2)
        {
            array1 = array1.Concat(new[] { array2 }).ToArray();
            return array1;
        }
        public static T[] MergeArray<T>(this T[] array1, T[] array2)
        {
            array1 = array1.Concat(array2).ToArray();
            return array1;
        }
        public static T[] RangeFrom<T>(this T[] items, T selection) where T : class
        {
            if (!items.Contains(selection)) return items;
            var returnList = new List<T>();
            for (var x = items.IndexOf(selection); x < items.Length; x++)
                returnList.Add(items[x]);
            return returnList.ToArray();
        }
        public static T[] RangeUnderIndex<T>(this T[] items, int selection) where T : class
        {
            var returnList = new List<T>();
            for (var x = 0; x < items.Length && x < selection; x++)
                returnList.Add(items[x]);
            return returnList.ToArray();
        }
        public static IEnumerable<T> SetComplement<T>(this IEnumerable<T> universe, IEnumerable<T> setA)
        {
            if (universe == null) return new T[0];
            return setA == null ? universe : universe.Where(item => !setA.Contains(item));
            //  var items = from item in universe where (setA.Contains(item) == false) select item;
        }

        public static string[] ToSortedArray(this IEnumerable<string> items)
        {
            var returnItems = items.ToArray();
            Array.Sort(returnItems, StringComparer.CurrentCulture);
            return returnItems;
        }
        public static void AddUnique<T>(this List<T> list, T item)
        {
            if (list.Contains(item)) return;
            list.Add(item);
        }
        public static void AddUnique<T>(this ObservableCollection<T> list, T item)
        {
            if (list.Contains(item)) return;
            list.Add(item);
        }
        public static void AddUnique<TKey, T>(this Dictionary<TKey, T> list, TKey key, T item)
        {
            if (list.ContainsKey(key))
                list[key] = item;
            else
                list.Add(key, item);
        }
        public static void AddRangeUnique<T>(this List<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
                list.AddUnique(item);
        }
                public static void AddRange<T>(this ObservableCollection<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
                list.Add(item);
        }
              public static void AddRangeUnique<T>(this ObservableCollection<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
                list.AddUnique(item);
        }
              public static void RemoveAll<T>(this ObservableCollection<T> list, Predicate<T> rule)
              {
                    (from item in list where rule(item) select item).ForEach(i=>list.Remove(i)); 
              }
        public static void Update<T, TE>(this Dictionary<T, TE> dict, T key, TE value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(key, value);
        }
        public static bool RemoveTry<T>(this List<T> list, T item)
        {
            return list.Remove(item);
        }
        public static bool RemoveTry<T>(this ObservableCollection<T> list, T item)
        {
            return list.Remove(item);
        }
        public static bool RemoveTry<T, TE>(this Dictionary<T, TE> list, T item)
        {
            return list.Remove(item);
        }
        public static void RemoveRangeTry<T>(this List<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
                list.RemoveTry(item);
        }
        public static void RemoveRangeTry<T>(this ObservableCollection<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
                list.RemoveTry(item);
        }
        public static void Replace<T>(this List<T> currentList, T existing, T replacement)
        {
            if (currentList.Contains(existing))
                currentList.Remove(existing);
            currentList.Add(replacement);
        }
        public static T MaxBy<T, TK>(this IEnumerable<T> source, Func<T, TK> selector)
        {
            return source.MaxBy(selector, Comparer<TK>.Default);
        }
        public static T MaxBy<T, TK>(this IEnumerable<T> source, Func<T, TK> selector, IComparer<TK> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            if (comparer == null) throw new ArgumentNullException("comparer");
            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var max = sourceIterator.Current;
                var maxKey = selector(max);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, maxKey) <= 0) continue;
                    max = candidate;
                    maxKey = candidateProjected;
                }
                return max;
            }
        }
        public static int IndexOf<T>(this T[] items, T selection) where T : class
        {
            return Array.IndexOf(items, selection);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items.ToArray())
            {
                action(item);
            }
        }
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items )
        {
            var collection = new ObservableCollection<T>();
            foreach (var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }
        public static T RandomItem<T>(this IList<T> items)
        {
            if (items == null) return default(T);

            var length = items.Count();
            var index = DiceRoll.RollRandom(0, length - 1);
            return items[index];
        }

        public static List<T> GetRandomizedList<T>(this IEnumerable<T> sourceItems)
        {
            var sourceList = sourceItems.ToList(); //make a copy
            var returnList = new List<T>();
            while (sourceList.Any())
            {
                var item = sourceList.RandomItem();
                sourceList.Remove(item);
                returnList.Add(item);
            }

            return returnList;
        }
    }
}