using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PolyhydraGames.Extensions
{
    public static class AssemblyExtensions
    {
        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }

        public static Stream GetEmbeddedResourceStream(this Type type, string address)
        {
            var assembly = type.GetAssembly();
            var fullAddress = assembly.GetAssemblyName() + "." + address;
         return assembly.GetManifestResourceStream(fullAddress);
            
        }
        public static string GetEmbeddedResourceString(this Type type,string address)
        {
            var text = "";
            using (var reader = new System.IO.StreamReader(GetEmbeddedResourceStream(type,address)))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

        public static string GetAssemblyName(this Assembly assembly)
        {
            return assembly.GetName().Name;
        }

        public static string GetAssemblyName(this Type type)
        {
            return type.GetAssembly().GetName().Name;
        }

        public static IList<Type> GetTypesEndingWith(this Assembly[] assembly, string name)
        {
            var pages = new List<Type>();
            foreach (var item in assembly)
            {
                pages.AddRange(item.CreatableTypes().EndingWith(name));
            }
            return pages;
        }

        public static string[] GetResources(this Assembly assembly)
        {

            var resources = assembly.GetManifestResourceNames();
            return resources;
        }

        public static IList<Type> GetTypesEndingWith(this Assembly assembly, string name)
        {
            var pages = new List<Type>();
            pages.AddRange(assembly.CreatableTypes().EndingWith(name));
            return pages;
        }
    }
}