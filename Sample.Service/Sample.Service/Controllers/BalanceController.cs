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
    public class BalanceController : CustomApiController
    {
        private readonly IBalanceService service;

        public BalanceController(IBalanceService service)
        {
            this.service = service;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(BalanceOutputDTO), StatusCodes.Status200OK)]
        public Task<IActionResult> GetBalance()
        {
            return ResultAsync<Balance, BalanceOutputDTO>(service.GetBalance());
        }

        [HttpPost("deposit")]
        [ProducesResponseType(typeof(BalanceOutputDTO), StatusCodes.Status200OK)]
        public Task<IActionResult> Deposit(BalanceInputDTO dto)
        {
            return ResultAsync<Balance, BalanceOutputDTO>(service.Deposit(dto.Amount));
        }

        [HttpPost("withdraw")]
        [ProducesResponseType(typeof(BalanceOutputDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Withdraw(BalanceInputDTO dto)
        {
            return ResultAsync<Balance, BalanceOutputDTO>(service.Withdraw(dto.Amount));
        }
    }
}