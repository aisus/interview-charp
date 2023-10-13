using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Team.Extensions.Utility.DAL
{
    public static class DbContextExtensions
    {
        public static async Task ReloadCollection<T, TProp>(this DbContext context, T record,
            Expression<Func<T, IEnumerable<TProp>>> propExpr)
            where T : class
            where TProp : class
        {
            var collectionEntry = context.Entry(record).Collection(propExpr);
            if (collectionEntry.CurrentValue != null)
            {
                foreach (var item in collectionEntry.CurrentValue)
                    context.Entry(item).State = EntityState.Detached;
                collectionEntry.CurrentValue = null;
            }

            collectionEntry.IsLoaded = false;
            await collectionEntry.LoadAsync();
        }
    }
}