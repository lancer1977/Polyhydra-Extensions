using System;
using Microsoft.Extensions.Logging;

namespace PolyhydraGames.Extensions;

public static class LogExtensions
{
    /// <summary>
    /// Logs an exception and then throws it.
    /// Signature returns T to be used in a function, but it's not used.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="logger"></param>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static T LogAndThrow<T>(this ILogger logger, Exception ex)
    {
        logger.LogError(ex, ex.Message);
        throw ex;
    }

    public static T LogAndThrow<T>(this ILogger logger, string message)
    {
        var exception = new Exception(message);
        logger.LogError(exception, message);
        throw exception;
    }

    public static Exception LogAndThrow(this ILogger logger, Exception ex)
    {
        LogAndThrow<string>(logger, ex);
        return ex;
    }

    public static Exception LogAndThrow(this ILogger logger, string message)
    {
        var exception = new Exception(message);
        logger.LogError(exception, message);
        throw exception;
    }
}