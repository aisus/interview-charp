using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sample.Extensions.Infrastrcture;

namespace Sample.Extensions.DAL
{
    public interface IAsyncOrderedQueryService<T> where T : class, IAuditableEntity
    {
        Task<ApiResult<CollectionOutputModel<TModel>>> GetPageAsync<TModel, TKey>(PageModel pageModel, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector, bool descending);
    }
}