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
            
            var entityWithDate = record as IEntityWithCreatedDate;
            if (entityWithDate != null)
            {
                entityWithDate.CreatedDate = DateTime.UtcNow;
            }

            repository.Add(record);
            return await context.SaveChangesAsync() > 0
                ? ApiResult<T>.Success(record)
                : ApiResult<T>.Error("Error on record insert");
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
                return ApiResult<T>.NotFound("Record not found", nameof(guid));
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
            if (record == null) return ApiResult<T>.NotFound("Record not found", nameof(guid));
            context.Entry(record).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            return ApiResult<T>.Success("Record deleted");
        }
    }
}