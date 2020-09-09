using System;
using System.Collections.Generic;
using System.Linq;

namespace BHub.Lib.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> collection, Func<T, bool> predicate, bool execute)
        {
            if (execute)
            {
                return collection.Where(predicate);
            }

            return collection;
        }
    }
}