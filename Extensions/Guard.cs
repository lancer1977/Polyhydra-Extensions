using System;
using System.Runtime.CompilerServices;

namespace PolyhydraGames.Extensions
{
    public static class Guard
    {
        public static void AgainstNull<T>(T value, [CallerMemberName] string name = "")
        {
            if (value == null) throw new ArgumentNullException($"{name} had a illegal null.");
        }
    }
}