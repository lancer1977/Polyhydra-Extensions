using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PolyhydraGames.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns all the creatable types in an assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> CreatableTypes(this Assembly assembly)
        {
            return assembly
                .DefinedTypes
                //.Select(t => t.GetTypeInfo())
                .Where(t => !t.IsAbstract)
                .Where(t => t.DeclaredConstructors.Any(c => !c.IsStatic && c.IsPublic))
                .Select(t => t.AsType());
        }

        /// <summary>
        /// Selects types ending with
        /// </summary>
        /// <param name="types"></param>
        /// <param name="endingWith"></param>
        /// <returns></returns>
        public static IEnumerable<Type> EndingWith(this IEnumerable<Type> types, string endingWith)
        {
            return types.Where(x => x.Name.EndsWith(endingWith));
        }

        /// <summary>
        /// Selects types starting with
        /// </summary>
        /// <param name="types"></param>
        /// <param name="endingWith"></param>
        /// <returns></returns>
        public static IEnumerable<Type> StartingWith(this IEnumerable<Type> types, string endingWith)
        {
            return types.Where(x => x.Name.StartsWith(endingWith));
        }

        /// <summary>
        /// Selects types containing a string
        /// </summary>
        /// <param name="types"></param>
        /// <param name="containing"></param>
        /// <returns></returns>
        public static IEnumerable<Type> Containing(this IEnumerable<Type> types, string containing)
        {
            return types.Where(x => x.Name.Contains(containing));
        }

        /// <summary>
        /// Selects types in namespace
        /// </summary>
        /// <param name="types"></param>
        /// <param name="namespaceBase"></param>
        /// <returns></returns>
        public static IEnumerable<Type> InNamespace(this IEnumerable<Type> types, string namespaceBase)
        {
            return types.Where(x => x.Namespace != null && x.Namespace.StartsWith(namespaceBase));
        }
        
        /// <summary>
        /// Exculudes types provided
        /// </summary>
        /// <param name="types"></param>
        /// <param name="except"></param>
        /// <returns></returns>
        public static IEnumerable<Type> Except(this IEnumerable<Type> types, params Type[] except)
        {
            // optimisation - if we have 3 or more except cases, then use a dictionary
            if (except.Length >= 3)
            {
                var lookup = except.ToDictionary(x => x, x => true);
                return types.Where(x => !lookup.ContainsKey(x));
            }
            else
            {
                return types.Where(x => !except.Contains(x));
            }
        }

 
         
    }
}