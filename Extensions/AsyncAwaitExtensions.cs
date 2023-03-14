using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PolyhydraGames.Extensions
{
    public static class AsyncTaskExtensions
    {
        public static async Task<List<T>> ToListAsync<T>(this Func<Task<IEnumerable<T>>> source)
        {
            var enumerable = await source.Invoke();
            return enumerable.ToList();
        }

        public static async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> source)
        {
            var enumerable = await source;
            return enumerable.ToList();
        }

#if NET7_0_OR_GREATER
        public static async Task<IEnumerable<T>> DistinctAsync<T>(this Task<IEnumerable<T>> source, Func<T, object> orderer)
        {
            var enumerable = await source;
            return enumerable.DistinctBy(orderer);
        }
#endif
        public static async Task<IEnumerable<T>> OrderByAsync<T>(this Task<IEnumerable<T>> source, Func<T, object> orderer)
        {
            var enumerable = await source;
            return enumerable.OrderBy(orderer);
        }

        public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(this Task<IEnumerable<T>> source, Func<T, TResult> selector)
        {
            var enumerable = await source;
            return enumerable.Select(selector);
        }

        public static async Task<bool> AnyAsync<T>(this Task<IEnumerable<T>> source)
        {
            var enumerable = await source;
            return enumerable.Any();
        }



        public static async Task ExecuteAsync(this Func<Task> act, int times = 1)
        {
            if (times <= 0)
                return;
            for (var index = 0; index < times; ++index)
                await act();
        }



        public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(this Task<List<T>> source, Func<T, TResult> selector) where T : new()
        {
            var enumerable = await source;
            return enumerable.Select(selector);
        }

    }
}
