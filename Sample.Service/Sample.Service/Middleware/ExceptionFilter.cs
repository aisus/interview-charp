using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sample.Extensions.Infrastrcture;

namespace Sample.Service.Middleware
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new MessageResult{Message = context.Exception.Message}){StatusCode = 500};
        }
    }
}