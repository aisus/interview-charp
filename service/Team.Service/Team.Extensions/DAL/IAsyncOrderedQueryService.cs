using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Team.Extensions.Infrastrcture;

namespace Team.Extensions.DAL
{
    public interface IAsyncOrderedQueryService<T> where T : class, IEntityWithCreatedDate
    {
        Task<ApiResult<CollectionOutputModel<TModel>>> GetPageAsync<TModel, TKey>(PageModel pageModel, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector, bool descending);
    }
}