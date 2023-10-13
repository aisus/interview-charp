using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Team.Extensions.Infrastrcture;

namespace Team.Extensions.Web
{
    public abstract class EntityApiController<T> : CustomApiController where T : class, IEntity
    {
        protected IActionResult MapEntity<TOut>(ApiResult<T> result)
        {
            if (result)
                return Ok(result.Entity.Adapt<TOut>());
            else
                return ApiResult(result);
        }

        protected async Task<IActionResult> MapEntityAsync<TOut>(Task<ApiResult<T>> resultTask)
        {
            return MapEntity<TOut>(await resultTask);
        }
    }
}