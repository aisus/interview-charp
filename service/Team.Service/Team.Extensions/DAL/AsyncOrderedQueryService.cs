using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Team.Extensions.Infrastrcture;
using Team.Extensions.Utility;
using Team.Extensions.Utility.DAL;

namespace Team.Extensions.DAL
{
    public class AsyncOrderedQueryService<T> : IAsyncOrderedQueryService<T> where T : EntityWithCreatedDate
    {
        protected readonly DbContext context;
        protected readonly DbSet<T> repository;

        public AsyncOrderedQueryService(DbContext ctx)
        {
            context = ctx;
            repository = ctx.Set<T>();
        }

        public async Task<ApiResult<CollectionOutputModel<TModel>>> GetPageAsync<TModel, TKey>(PageModel pageModel,
            Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector, bool descending)
        {
            var query = repository.WhereIf(filter, filter != null);

            if (descending)
            {
                query = query.OrderByDescending(keySelector);
            }
            else
            {
                query = query.OrderBy(keySelector);
            }

            var result = await query
                .GetPageAsync<T, TModel>(pageModel);
                
            return result != null
                ? ApiResult<CollectionOutputModel<TModel>>.Success(result)
                : ApiResult<CollectionOutputModel<TModel>>.NotFound();
        }
    }
}