using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PolyhydraGames.Extensions;

public static class TryHelpers
{
    public static async Task Try(this Task func)
    {
        try
        {
            await func;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public static async Task<T> Try<T>(this Task<T> func)
    {
        try
        {
            return await func;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return default(T);
    }

}