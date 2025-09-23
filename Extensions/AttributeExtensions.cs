using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PolyhydraGames.Extensions;

public static class AttributeExtensions
{
    public static IEnumerable<Type> GetTypesFromAttribute<T>(this Assembly assembly)
    {
        var result = assembly.DefinedTypes
            .Where(i => i.GetCustomAttributes(false).Any(atr => atr.GetType() == typeof(T))).Cast<Type>();
        return result;
    }

    public static IEnumerable<Type> GetTypesWithHelpAttribute<T>(this Assembly assembly)
    {
        foreach (var type in assembly.DefinedTypes)
        {
            if (type.GetCustomAttributes(typeof(T), true).Any())
            {
                yield return type.GetType();
            }
        }
    }
}