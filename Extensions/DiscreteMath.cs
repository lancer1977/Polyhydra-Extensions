using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyhydraGames.Extensions
{
    public static class DiscreteMath
    {
        //p(5,2)
        //final = 5
        public static long Factorial(int size, int take)
        {
            long final = size * (size - 1); 
            if (take > 0)
            {
                final += Factorial(size - 1, take - 1);
            }
        

            return final;

        }

        public static long SumArray(string number, string items)
        {
            return SumArray(Convert.ToInt32(number), items.Split(' ').Select(i => Convert.ToInt64(i)).ToArray());
        }

        public static long SumArray(int number, IList<long> items)
        {
            if (items.Count( ) > number) throw new ArgumentOutOfRangeException( nameof(number),"was less then items count");
            return items.Sum(); 
        }
        
    }
}