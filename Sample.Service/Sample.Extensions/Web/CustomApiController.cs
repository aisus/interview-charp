using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Sample.Extensions.Infrastrcture;

namespace Sample.Extensions.Web
{
    public abstract class CustomApiController : ControllerBase
    {
        protected IActionResult Result<T>(ApiResult<T> result) where T : class
        {
            return result ? Ok(result.Entity) : StatusCode((int)result.ResultCode, new MessageResult(){Message = result.Message});
        }

        protected IActionResult Result<T, TModel>(ApiResult<T> result) where T : class
        {
            return result ? Ok(result.Entity.Adapt<TModel>()) : StatusCode((int)result.ResultCode, new MessageResult(){Message = result.Message});
        }

        protected IActionResult Success(bool success)
        {
            return success ? Ok() : NotFound();
        }

        protected IActionResult BadRequest(string message)
        {
            return StatusCode(400, new MessageResult(){Message = message});
        }

        protected async Task<IActionResult> ResultAsync<T>(Task<ApiResult<T>> task) where T : class
        {
            return Result(await task);
        }

        protected async Task<IActionResult> ResultAsync<T, TModel>(Task<ApiResult<T>> task) where T : class
        {
            return Result<T, TModel>(await task);
        }
    }
}