using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolyhydraGames.Extensions;

public static class AsyncLinqExtensions
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