using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PolyhydraGames.Extensions;

public static class IocExtensions
{
    public static bool HasAttribute<T>(this Type type)
    {
        return typeof(T).IsAny(Attribute.GetCustomAttributes(type).Select(x => x.GetType()).ToArray());
    }

    public static IEnumerable<Type> ThatImplement<T>(this IEnumerable<Type> types)
    {
        return types.Where(x => x.IsAssignableTo<T>());
    }

    public static IEnumerable<Type> ThatDoNotImplement<T>(this IEnumerable<Type> types)
    {
        return types.Where(x => !x.IsAssignableTo<T>());
    }

    /// <summary>
    /// Determines whether this type is assignable to T.
    /// </summary>
    /// <param name="type">The type to test.</param>
    /// <returns>True if this type is assignable to references of type T; otherwise, False.</returns>
    public static bool IsAssignableTo<T>(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return typeof(T).IsAssignableFrom(type);
    }

    public static IEnumerable<Type> ThatHaveAttribute<T>(this IEnumerable<Type> types)
    {
        return types.Where(x => x.HasAttribute<T>());
    }

    public static object? CreateDefault(this Type type)
    {
        if (type == null)
            return null;

        if (!type.GetTypeInfo().IsValueType)
            return null;

        if (Nullable.GetUnderlyingType(type) != null)
            return null;

        return Activator.CreateInstance(type);
    }
}