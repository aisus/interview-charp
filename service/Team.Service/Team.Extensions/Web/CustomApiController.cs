using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Team.Extensions.Infrastrcture;

namespace Team.Extensions.Web
{
    public abstract class CustomApiController : ControllerBase
    {
        protected string QueryString => Request.QueryString.Value;

        protected IActionResult MessageApiResult(ApiResult<MessageResult> result)
        {
            return StatusCode((int)result.ResultCode, result ? result.Entity : MessageResult.FromApiResult(result));
        }

        protected IActionResult ApiResult<T>(ApiResult<T> result) where T : class
        {
            return StatusCode((int)result.ResultCode, MessageResult.FromApiResult(result));
        }

        protected IActionResult Model<T>(T entity) where T : class
        {
            if (entity != null)
                return Ok(entity);
            else
                return NotFound("Record not found");
        }

        protected IActionResult ModelResult<T>(ApiResult<T> result) where T : class
        {
            return result ? Ok(result.Entity) : ApiResult(result);
        }

        protected IActionResult Success(bool success)
        {
            return success ? Ok() : NotFound();
        }

        protected IActionResult BadRequestResult(string message)
        {
            return StatusCode(400, message);
        }


        protected async Task<IActionResult> ModelAsync<T>(Task<T> entityTask) where T : class
        {
            return Model(await entityTask);
        }

        protected async Task<IActionResult> ModelAsync<T>(Task<ApiResult<T>> entityTask) where T : class
        {
            var result = await entityTask;
            return StatusCode((int)result.ResultCode, result ? result.Entity : MessageResult.FromApiResult(result));
        }

        protected async Task<IActionResult> ApiResultAsync<T>(Task<ApiResult<T>> resultTask) where T : class
        {
            return ApiResult(await resultTask);
        }

        protected async Task<IActionResult> ModelResultAsync<T>(Task<ApiResult<T>> resultTask) where T : class
        {
            return ModelResult(await resultTask);
        }

        protected async Task<IActionResult> SuccessAsync(Task<bool> boolTask)
        {
            return Success(await boolTask);
        }
    }
}