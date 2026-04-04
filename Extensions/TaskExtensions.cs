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

    public static async Task WithTimeout(this Task task, int timeoutMilliseconds)
    {
        if (task is null) throw new ArgumentNullException(nameof(task));

        var delayTask = Task.Delay(timeoutMilliseconds);
        var completed = await Task.WhenAny(task, delayTask).ConfigureAwait(false);
        if (completed == delayTask)
            throw new TimeoutException($"The task did not complete within {timeoutMilliseconds}ms.");

        await task.ConfigureAwait(false);
    }

    public static async Task<T> WithTimeout<T>(this Task<T> task, int timeoutMilliseconds)
    {
        if (task is null) throw new ArgumentNullException(nameof(task));

        var delayTask = Task.Delay(timeoutMilliseconds);
        var completed = await Task.WhenAny(task, delayTask).ConfigureAwait(false);
        if (completed == delayTask)
            throw new TimeoutException($"The task did not complete within {timeoutMilliseconds}ms.");

        return await task.ConfigureAwait(false);
    }
}
