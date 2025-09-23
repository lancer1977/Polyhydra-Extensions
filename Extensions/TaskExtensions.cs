using System;
using System.Threading.Tasks;

namespace PolyhydraGames.Extensions;

public static class TaskExtensions
{
    public static void FireAndForget(this Func<Task> func, Action<Exception>? onError = null)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                // Or log to ILogger / Console.WriteLine / etc.
            }
        });
    }

    public static void FireAndForget(this Task task, Action<Exception>? onError = null)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        });
    }
}