using System.Collections.Generic;

namespace PolyhydraGames.Extensions
{
    public static class BogoSorter
    {
        public static List<T> Randomize<T>(this List<T> originalList) where T : class
        {
            List<T> randoList = new List<T>();
            randoList.AddRange(originalList);
            while (!randoList.IsSorted(originalList))
            {
                randoList.Shuffle();
            }

            return randoList;
        }

        private static bool IsSorted<T>(this IList<T> newList, IList<T> oldList) where T : class
        {
            if (newList.Count <= 1)
                return true;
            for (int i = 1; i < newList.Count; i++)
                if (newList[i] == oldList[i]) return false;
            return true;
        }
    }
}