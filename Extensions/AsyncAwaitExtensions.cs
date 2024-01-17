using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
namespace PolyhydraGames.Extensions
{



    public static class AsyncExtensions
    {
#if NET6_0_OR_GREATER
        public static async Task<IEnumerable<T>> DistinctAsync<T>(this Task<IEnumerable<T>> source, Func<T, object> orderer)
        {
            var enumerable = await source;
            return enumerable.DistinctBy(orderer);
        }
#endif
        public static ILogger Log { get; set; }

        /// <summary>
        /// Gets the required resource async  and pass it to the function.
        /// If an error occurs this is logged to the logger
        /// </summary>
        /// <typeparam name="T">Return value</typeparam>
        /// <typeparam name="T2">Get Resource</typeparam>
        /// <param name="clientTask">Function to grab the dependency</param>
        /// <param name="func">behavior to run</param>
        /// <returns></returns>
        public static async Task RunWithLog<T>(this Task<T> clientTask, Func<T, Task> func)
        {
            try
            {
                var client = await clientTask;
                await func.Invoke(client);
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, ex.Message);
                throw;
            }

        }

        /// <summary>
        /// Gets the required resource async  and pass it to the function of T.
        /// If an error occurs this is logged to the logger
        /// </summary>
        /// <typeparam name="TResult">Return value</typeparam>
        /// <typeparam name="T2">Get Resource</typeparam>
        /// <param name="clientTask">Function to grab the dependency</param>
        /// <param name="func">behavior to run</param>
        /// <returns></returns>
        public static async Task<TResult> RunWithLog<TResult, T>(this Task<T> clientTask, Func<T, Task<TResult>> func)
        {
            try
            {
                var client = await clientTask;
                return await func.Invoke(client);
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, ex.GetMessages());
                throw;
            }
        }


        /// <summary>
        /// If the task completes, return this value
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static async Task<TResult> Return<TResult>(this Task task, TResult defaultValue)
        {
            await task;
            return defaultValue;
        }

        public static string GetMessages(this Exception ex)
        {
            return ex.InnerException == null ? ex.Message : ex.Message + "\n" + ex.InnerException.GetMessages();
        }
    }
    public static class AsyncTaskExtensions
    {
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




        public static async Task<TResult> FirstOrDefaultAsync<TResult>(this Task<IEnumerable<TResult>> itemTask, Func<TResult, bool> predicate)
        {

            var item = await itemTask;
            return item.FirstOrDefault(predicate);

        }


        public static async Task<List<TResult>> ToListAsync<TResult>(this Func<Task<IEnumerable<TResult>>> source)
        {
            var enumerable = await source.Invoke();
            return enumerable.ToList();
        }

        public static async Task<List<TResult>> ToListAsync<TResult>(this Task<IEnumerable<TResult>> source)
        {
            var enumerable = await source;
            return enumerable.ToList();
        }

        //[Obsolete("Inefficient, avoid use")]
        //public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(this Task<List<T>> source, Func<T, TResult> selector) where T : new()
        //{
        //    var enumerable = await source;
        //    return enumerable.Select(selector);
        //}

        //[Obsolete("Inefficient, avoid use")]
        //public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(this Task<IEnumerable<T>> source, Func<T, TResult> selector)
        //{
        //    var enumerable = await source;
        //    return enumerable.Select(selector);
        //}


        //[Obsolete("Inefficient, avoid use")]
        //public static async Task<IEnumerable<T>> WhereAsync<T>(this Task<IEnumerable<T>> itemTask, Func<T, bool> predicate)
        //{

        //    var item = await itemTask;
        //    return item.Where(predicate);

        //}

        //[Obsolete("Inefficient, avoid use")]
        //public static async Task<IEnumerable<T>> OrderByAsync<T>(this Task<IEnumerable<T>> source, Func<T, object> orderer)
        //{
        //    var enumerable = await source;
        //    return enumerable.OrderBy(orderer);
        //}
    }
}
