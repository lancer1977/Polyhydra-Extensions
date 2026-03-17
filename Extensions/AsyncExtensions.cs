using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolyhydraGames.Extensions;

public static class AsyncExtensions
{
    // Default retry settings
    public static int MaxRetries { get; set; } = 3;
    public static int BaseDelayMs { get; set; } = 1000;

#if NET6_0_OR_GREATER
    public static async Task<IEnumerable<T>> DistinctAsync<T>(this Task<IEnumerable<T>> source, Func<T, object> orderer)
    {
        var enumerable = await source;
        return enumerable.DistinctBy(orderer);
    }
#endif
    public static ILogger Log { get; set; }

    /// <summary>
    /// Determines if an exception is retryable (rate limit or server error)
    /// </summary>
    private static bool IsRetryable(Exception ex)
    {
        // Check for SpotifyAPI.Web exception types (v7.0+)
        if (ex.GetType().Name is "APIException" or "TooManyRequestsException" or "UnauthorizedException" or "NotFoundException" or "BadRequestException")
        {
            return ex.GetType().Name switch
            {
                "TooManyRequestsException" => true,  // 429
                "UnauthorizedException" => false,    // 401 - auth issue
                "NotFoundException" => false,        // 404 - resource doesn't exist
                "BadRequestException" => false,       // 400 - bad request
                _ => true // APIException base class
            };
        }
        // Fallback for other exception types - check message for server errors
        return ex.Message.Contains("5") || ex.Message.Contains("502") || ex.Message.Contains("503") || ex.Message.Contains("504");
    }

    /// <summary>
    /// Gets the retry delay from APIException if available
    /// </summary>
    private static TimeSpan GetRetryDelay(Exception ex)
    {
        // Try to find RetryAfter property (present in TooManyRequestsException)
        var retryAfterProp = ex.GetType().GetProperty("RetryAfter");
        if (retryAfterProp != null && retryAfterProp.GetValue(ex) is TimeSpan ts)
        {
            return ts;
        }
        return TimeSpan.FromMilliseconds(BaseDelayMs);
    }

    /// <summary>
    /// Gets the required resource async  and pass it to the function.
    /// If an error occurs this is logged to the logger
    /// </summary>
    /// <typeparam name="T">Return value</typeparam>
    /// <typeparam name="T2">Get Resource</typeparam>
    /// <param name="clientTask">Function to grab the dependency</param>
    /// <param name="func">behavior to run</param>
    /// <param name="onError">Callback for exceptions</param>
    /// <returns></returns>
    public static async Task RunWithLog<T>(this Task<T> clientTask, Func<T, Task> func, Action<Exception>? onError = null)
    {
        var attempt = 0;
        while (true)
        {
            try
            {
                var client = await clientTask;
                await func.Invoke(client);
                return;
            }
            catch (Exception ex)
            {
                attempt++;
                var isRetryable = IsRetryable(ex);
                var delay = GetRetryDelay(ex);
                
                Log.LogCritical(ex, $"[Attempt {attempt}] {ex.GetMessages()}");
                onError?.Invoke(ex);

                if (!isRetryable || attempt >= MaxRetries)
                {
                    throw;
                }

                Log.LogInformation("Retrying in {Delay}ms (attempt {Attempt}/{MaxRetries})", delay.TotalMilliseconds, attempt, MaxRetries);
                await Task.Delay(delay);
            }
        }
    }

    /// <summary>
    /// Gets the required resource async and pass it to the function of T.
    /// If an error occurs this is logged to the logger. Returns default(TResult) on error instead of throwing.
    /// </summary>
    /// <typeparam name="TResult">Return value</typeparam>
    /// <typeparam name="T2">Get Resource</typeparam>
    /// <param name="clientTask">Function to grab the dependency</param>
    /// <param name="func">behavior to run</param>
    /// <param name="onError">Callback for exceptions</param>
    /// <returns></returns>
    public static async Task<TResult> RunWithLog<TResult, T>(this Task<T> clientTask, Func<T, Task<TResult>> func, Action<Exception>? onError = null)
    {
        var attempt = 0;
        while (true)
        {
            try
            {
                var client = await clientTask;
                return await func.Invoke(client);
            }
            catch (Exception ex)
            {
                attempt++;
                var isRetryable = IsRetryable(ex);
                var delay = GetRetryDelay(ex);
                
                Log.LogCritical(ex, $"[Attempt {attempt}] {ex.GetMessages()}");
                onError?.Invoke(ex);

                if (!isRetryable || attempt >= MaxRetries)
                {
                    Log.LogWarning("Max retries reached or non-retryable error. Returning default.");
                    return default!;
                }

                Log.LogInformation("Retrying in {Delay}ms (attempt {Attempt}/{MaxRetries})", delay.TotalMilliseconds, attempt, MaxRetries);
                await Task.Delay(delay);
            }
        }
    }

    /// <summary>
    /// Gets the required resource async and pass it to the function of T.
    /// If an error occurs this is logged to the logger - THROWS on error (legacy behavior)
    /// </summary>
    /// <typeparam name=" TResult">Return value</typeparam>
    /// <typeparam name="T2">Get Resource</typeparam>
    /// <param name="clientTask">Function to grab the dependency</param>
    /// <param name="func">behavior to run</param>
    /// <returns></returns>
    public static async Task<TResult> RunWithLog<TResult>(this Task<TResult> clientTask, Func<TResult, Task<TResult>> func, Action<Exception>? onError = null)
    {
        var attempt = 0;
        while (true)
        {
            try
            {
                var client = await clientTask;
                return await func.Invoke(client);
            }
            catch (Exception ex)
            {
                attempt++;
                var isRetryable = IsRetryable(ex);
                var delay = GetRetryDelay(ex);
                
                Log.LogCritical(ex, $"[Attempt {attempt}] {ex.GetMessages()}");
                onError?.Invoke(ex);

                if (!isRetryable || attempt >= MaxRetries)
                {
                    throw;
                }

                Log.LogInformation("Retrying in {Delay}ms (attempt {Attempt}/{MaxRetries})", delay.TotalMilliseconds, attempt, MaxRetries);
                await Task.Delay(delay);
            }
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
