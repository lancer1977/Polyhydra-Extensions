using System;
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
    }
}