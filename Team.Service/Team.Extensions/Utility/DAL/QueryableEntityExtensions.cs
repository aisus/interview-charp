using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Team.Extensions.Infrastrcture;

namespace Team.Extensions.Utility.DAL
{
    public static class QueryableEntityExtensions
    {
        public static T GetById<T>(this IQueryable<T> queryable, Guid id) where T : IEntity
        {
            return queryable.FirstOrDefault(x => x.Id == id);
        }

        public static Task<T> GetByIdAsync<T>(this IQueryable<T> queryable, Guid id) where T : IEntity
        {
            return queryable.FirstOrDefaultAsync(x => x.Id == id);
        }

        public static IQueryable<T> WithId<T>(this IQueryable<T> queryable, Guid id) where T : IEntity
        {
            return queryable.Where(x => x.Id == id);
        }

        public static bool Exists<T>(this IQueryable<T> queryable, Guid id) where T : IEntity
        {
            return queryable.WithId(id).Any();
        }

        public static Task<bool> ExistsAsync<T>(this IQueryable<T> queryable, Guid id) where T : IEntity
        {
            return queryable.WithId(id).AnyAsync();
        }
    }
}