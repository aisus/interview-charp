using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sample.Business.DTO;
using Sample.DAL.Models;
using Sample.Extensions;
using Sample.Extensions.DAL;
using Sample.Extensions.Infrastrcture;
using Sample.Business.Services.Game;

namespace Sample.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IBalanceService balanceService;
        private readonly IOperationService operationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IGameStrategy gameStrategy;

        public GameService(IBalanceService balanceService, IOperationService operationService, IHttpContextAccessor httpContextAccessor, IGameStrategy gameStrategy)
        {
            this.balanceService = balanceService;
            this.operationService = operationService;
            this.httpContextAccessor = httpContextAccessor;
            this.gameStrategy = gameStrategy;
        }

        public async Task<ApiResult<GameOutputDTO>> Play(GameInputDTO input)
        {
            var balanceCheck = await balanceService.Check(input.Stake);

            if (!balanceCheck)
            {
                return ApiResult<GameOutputDTO>.BadRequest(balanceCheck.Message);
            }

            var result = gameStrategy.Play(input);
            // TODO
            return ApiResult<GameOutputDTO>.Success(result);
        }
    }
}