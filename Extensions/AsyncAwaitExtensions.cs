using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PolyhydraGames.Extensions
{ 
    public static class AsyncTaskExtensions
    {
        public static async Task<IList<T>> ToListAsync<T>(this Func<Task<IEnumerable<T>>> source)
        {
            var enumerable = await source.Invoke();
            return enumerable.ToList();
        }

        public static async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> source)
        {
            var enumerable = await source;
            return enumerable.ToList();
        }
        public static async Task<List<T>> OrderByAsync<T>(this Task<IEnumerable<T>> source, Func<T, object> orderer)
        {
            var enumerable = await source;
            return enumerable.OrderBy(orderer).ToList();
        }
        
        public static async Task ExecuteAsync(this Func<Task> act, int times = 1)
        {
            if (times <= 0)
                return;
            for (var index = 0; index < times; ++index)
                await act();
        }
    }
}