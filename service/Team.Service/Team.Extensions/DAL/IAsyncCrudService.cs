using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Team.Extensions.Infrastrcture;

namespace Team.Extensions.DAL
{
    public interface IAsyncCrudService<T> where T : class, IEntity
    {
        Task<ApiResult<TModel>> GetRecordAsync<TModel>(Guid guid) where TModel : class;

        Task<ApiResult<CollectionOutputModel<TModel>>> GetPageAsync<TModel>(PageModel pageModel, Expression<Func<T, bool>> filter);

        Task<ApiResult<T>> CreateAsync<TModel>(TModel insertModel);

        Task<ApiResult<T>> UpdateAsync<TModel>(Guid guid, TModel updateModel);

        Task<ApiResult<T>> PatchAsync<TModel>(Guid guid, TModel patchModel);

        Task<ApiResult<T>> DeleteAsync(Guid guid);
    }
}