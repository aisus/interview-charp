using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sample.Extensions.Infrastrcture;

namespace Sample.Service.Middleware
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                string messages = string.Join("; ", context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => !string.IsNullOrWhiteSpace(x.ErrorMessage) ? x.ErrorMessage : x.Exception?.Message.ToString()));

                context.Result = new BadRequestObjectResult(new MessageResult { Message = messages });
            }
        }
    }
}