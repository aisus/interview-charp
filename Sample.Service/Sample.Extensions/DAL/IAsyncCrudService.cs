﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sample.Extensions.Infrastrcture;

namespace Sample.Extensions.DAL
{
    public interface IAsyncCrudService<T> where T : class, IEntity
    {
        Task<ApiResult<T>> GetRecordAsync(Guid guid);

        Task<ApiResult<CollectionOutputModel<TModel>>> GetPageAsync<TModel>(PageModel pageModel, Expression<Func<T, bool>> filter);

        Task<ApiResult<T>> FindAsync(Expression<Func<T, bool>> filter);

        Task<ApiResult<T>> CreateAsync<TModel>(TModel insertModel);

        Task<ApiResult<T>> UpdateAsync<TModel>(Guid guid, TModel updateModel);

        Task<ApiResult<T>> PatchAsync<TModel>(Guid guid, TModel patchModel);

        Task<ApiResult<T>> DeleteAsync(Guid guid);
    }
}