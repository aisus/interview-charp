using System;
using System.Linq;
using System.Linq.Expressions;

namespace Team.Extensions.Utility.DAL
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> expr,
            bool condition)
        {
            return condition ? queryable.Where(expr) : queryable;
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int page, int limit)
        {
            return queryable.Skip(limit * (page - 1)).Take(limit);
        }
    }
}