using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Sample.Extensions.Infrastrcture;

namespace Sample.Extensions.Utility
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, PageModel pageModel)
        {
            if (pageModel.Limit == 0)
                return queryable;
            else if (pageModel.Page <= 1)
                return queryable.Take(pageModel.Limit);
            else
                return queryable.Skip(pageModel.Offset).Take(pageModel.Limit);
        }

        public static async Task<CollectionOutputModel<TModel>> GetPageAsync<T, TModel>(this IQueryable<T> queryable,
            PageModel pageModel)
        {
            var noCount = pageModel.Page == 0;
            var results = await queryable
                .Page(pageModel)
                .ProjectToType<TModel>()
                .ToListAsync();
            var count = noCount ? results.Count : await queryable.CountAsync();
            return new CollectionOutputModel<TModel>(pageModel.Limit, pageModel.Page, count, results);
        }

        public static CollectionOutputModel<TModel> GetPage<T, TModel>(this IQueryable<T> queryable,
            PageModel pageModel)
        {
            var noCount = pageModel.Page == 0;
            var results = queryable
                .Page(pageModel)
                .ProjectToType<TModel>()
                .ToList();
            var count = noCount ? results.Count : queryable.Count();
            return new CollectionOutputModel<TModel>(pageModel.Limit, pageModel.Page, count, results);
        }
    }
}