using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Business.DTO;
using Sample.Business.Services;
using Sample.DAL.Models;
using Sample.Extensions.Infrastrcture;
using Sample.Extensions.Web;

namespace Sample.Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OperationsController : CustomApiController
    {
        private readonly IOperationService service;

        public OperationsController(IOperationService service)
        {
            this.service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CollectionOutputModel<OperationOutputDTO>), StatusCodes.Status200OK)]
        public Task<IActionResult> GetOperations()
        {
            return ResultAsync(service.GetPageAsync(new PageModel(), true));
        }
    }
}