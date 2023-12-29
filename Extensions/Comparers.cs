using System;
using System.Collections.Generic;

namespace PolyhydraGames.Extensions
{
    public static class Comparers
    {
        /// <summary>
        /// Find the most similar string in a list of strings, returns null if there wasn't a good match
        /// </summary>
        /// <param name="items"></param>
        /// <param name="value"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static string FindMostSimilar(this IEnumerable<string> items, string value, int threshold = 3)
        {
            var best = string.Empty;
            var bestDistance = int.MaxValue;
            foreach (var s in items)
            {
                var distance = LevenshteinDistance(value, s);
                if (distance >= bestDistance) continue;
                bestDistance = distance;
                best = s;
            }
            return bestDistance <= threshold ? best : string.Empty;
        }

        public static bool GetLevenshteinDistance(this string s1, string s2, int threshold = 3)
        {
            return s1.LevenshteinDistance(s2) <= threshold;
        }
        public static int LevenshteinDistance(this string s1, string s2)
        {
            var distance = new int[s1.Length + 1, s2.Length + 1];

            for (var i = 0; i <= s1.Length; i++)
            {
                for (var j = 0; j <= s2.Length; j++)
                {
                    if (i == 0)
                        distance[i, j] = j;
                    else if (j == 0)
                        distance[i, j] = i;
                    else
                        distance[i, j] = Math.Min(
                            Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                            distance[i - 1, j - 1] + (s1[i - 1] == s2[j - 1] ? 0 : 1));
                }
            }

            return distance[s1.Length, s2.Length];
        }
    }
}