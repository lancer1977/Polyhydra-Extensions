using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PolyhydraGames.Extensions
{
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
            //
            // Summary:
            //     Determines whether this type is assignable to T.
            //
            // Parameters:
            //   this:
            //     The type to test.
            //
            // Type parameters:
            //   T:
            //     The type to test assignability to.
            //
            // Returns:
            //     True if this type is assignable to references of type T; otherwise, False.
            public static bool IsAssignableTo<T>(this Type @this)
            {
                if (@this == null)
                {
                    throw new ArgumentNullException("this");
                }

                return typeof(T).IsAssignableFrom(@this);
            }

            //System.Attribute[] attrs = ;  // Reflection. 
            public static IEnumerable<Type> ThatHaveAttribute<T>(this IEnumerable<Type> types)
            {


                return types.Where(x => x.HasAttribute<T>());
            }
            public static object CreateDefault(this Type type)
            {
                if (type == null)
                {
                    return null;
                }

                if (!type.GetTypeInfo().IsValueType)
                {
                    return null;
                }

                if (Nullable.GetUnderlyingType(type) != null)
                    return null;

                return Activator.CreateInstance(type);
            }
        }
}