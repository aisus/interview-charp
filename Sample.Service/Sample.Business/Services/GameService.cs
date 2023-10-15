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
        private readonly IGameStrategy gameStrategy;

        public GameService(IBalanceService balanceService, IOperationService operationService, IGameStrategy gameStrategy)
        {
            this.balanceService = balanceService;
            this.operationService = operationService;
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
            if (result.GameWon)
            {
                await balanceService.Add(Math.Abs(result.BalanceChange));
            }
            else
            {
                await balanceService.Take(Math.Abs(result.BalanceChange));
            }

            await operationService.AddOperationEntry(balanceCheck.Entity.Id, DAL.Enums.OperationType.Game, result.BalanceChange, "Game");

            return ApiResult<GameOutputDTO>.Success(result);
        }
    }
}