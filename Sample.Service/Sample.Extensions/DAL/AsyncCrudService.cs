using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Sample.Extensions.Infrastrcture;
using Sample.Extensions.Utility;
using Sample.Extensions.Utility.DAL;

namespace Sample.Extensions.DAL
{
    public class AsyncCrudService<T> : IAsyncCrudService<T> where T : EntityBase
    {
        protected readonly DbContext context;
        protected readonly DbSet<T> repository;

        public AsyncCrudService(DbContext ctx)
        {
            context = ctx;
            repository = ctx.Set<T>();
        }

        public async Task<ApiResult<TModel>> GetRecordAsync<TModel>(Guid guid) where TModel : class
        {
            var result = await repository
                .WithId(guid)
                .ProjectToType<TModel>()
                .FirstOrDefaultAsync();
            return result != null
                ? ApiResult<TModel>.Success(result)
                : ApiResult<TModel>.NotFound();
        }

        public async Task<ApiResult<CollectionOutputModel<TModel>>> GetPageAsync<TModel>(PageModel pageModel,
            Expression<Func<T, bool>> filter)
        {
            var result = await repository.WhereIf(filter, filter != null)
                //.OrderByDescending(x => x.Id)
                .GetPageAsync<T, TModel>(pageModel);
            return result != null
                ? ApiResult<CollectionOutputModel<TModel>>.Success(result)
                : ApiResult<CollectionOutputModel<TModel>>.NotFound();
        }

        public async Task<ApiResult<T>> CreateAsync<TModel>(TModel insertModel)
        {
            var record = insertModel.Adapt<T>();
            
            var entityWithDate = record as IAuditableEntity;
            if (entityWithDate != null)
            {
                entityWithDate.CreatedDate = DateTime.UtcNow;
            }

            repository.Add(record);
            return await context.SaveChangesAsync() > 0
                ? ApiResult<T>.Success(record)
                : ApiResult<T>.BadRequest("Error on record insert");
        }

        private async Task<ApiResult<T>> UpdateRecordAsync<TModel>(T record, TModel updateModel)
        {
            updateModel.Adapt(record);
            await context.SaveChangesAsync();
            return ApiResult<T>.Success(record);
        }

        public async Task<ApiResult<T>> UpdateAsync<TModel>(Guid guid, TModel updateModel)
        {
            var record = await repository.GetByIdAsync(guid);
            if (record == null)
                return ApiResult<T>.NotFound();
            else
                return await UpdateRecordAsync(record, updateModel);
        }

        public Task<ApiResult<T>> PatchAsync<TModel>(Guid guid, TModel patchModel)
        {
            return UpdateAsync(guid, patchModel);
        }

        public async Task<ApiResult<T>> DeleteAsync(Guid guid)
        {
            var record = await repository.GetByIdAsync(guid);
            if (record == null) return ApiResult<T>.NotFound();
            context.Entry(record).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            return ApiResult<T>.Success("Record deleted");
        }
    }
}