using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PolyhydraGames.Extensions
{
    public static class LinqExtensions
    {
        static readonly Random _random = new Random();
        public static T RandomFirstOrDefault<T>(this IQueryable<T> q, Expression<Func<T, bool>> e)
        {
            q = q.Where(e);
            return q.Skip(_random.Next(q.Count())).FirstOrDefault();
        }

        public static T RandomFirstOrDefault<T>(this IQueryable<T> q)
        {
            return q.Skip(_random.Next(q.Count())).FirstOrDefault();
        }

 

        public static T RandomFirstOrDefault<T>(this IList<T> q)
        {
            return q.Skip(_random.Next(q.Count())).FirstOrDefault();
        }
    }
}