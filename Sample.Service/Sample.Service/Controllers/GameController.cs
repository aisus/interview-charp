using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Business.DTO;
using Sample.Business.Services;
using Sample.DAL.Models;
using Sample.Extensions.Web;

namespace Sample.Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : CustomApiController
    {
        private readonly IGameService service;

        public GameController(IGameService service)
        {
            this.service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GameOutputDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Deposit(GameInputDTO dto)
        {
            return ResultAsync(service.Play(dto));
        }
    }
}