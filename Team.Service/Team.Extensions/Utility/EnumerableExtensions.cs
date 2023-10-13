using System;
using System.Collections.Generic;
using System.Linq;

namespace Team.Extensions.Utility
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> enumerable, Func<T, bool> pred, bool cond)
        {
            return cond ? enumerable.Where(pred) : enumerable;
        }
    }
}