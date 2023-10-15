using Mapster;
using Moq;
using Sample.Business.DTO;
using Sample.Business.Services;
using Sample.Business.Services.Game;
using Sample.DAL.Models;
using Sample.Extensions.Infrastrcture;

namespace Sample.Service.Tests
{
    public class BaseFixture : IDisposable
    {
        public const decimal InitialBalance = 10000m;
        public Mock<IBalanceService> BalanceService;
        public Mock<IBalanceService> EmptyBalanceService;
        public Mock<IOperationService> OperationsService;
        public Mock<IGameStrategy> AlwaysWinGameStrategy;
        public Mock<IGameStrategy> AlwaysLoseGameStrategy;

        public BaseFixture()
        {
            Reset();
        }

        public void Reset()
        {
                        BalanceService = new Mock<IBalanceService>();
            EmptyBalanceService = new Mock<IBalanceService>();
            OperationsService = new Mock<IOperationService>();
            AlwaysWinGameStrategy = new Mock<IGameStrategy>();
            AlwaysLoseGameStrategy = new Mock<IGameStrategy>();

            BalanceService.Setup(x => x.Add(It.IsAny<Decimal>()))
                .ReturnsAsync((Decimal amount) =>
                {
                    return ApiResult<Balance>.Success(
                    new Balance
                    {
                        CurrentBalance = InitialBalance + amount
                    }
                );
                });

            BalanceService.Setup(x => x.Take(It.IsAny<Decimal>()))
                .ReturnsAsync((Decimal amount) =>
                {
                    return ApiResult<Balance>.Success(
                    new Balance
                    {
                        CurrentBalance = InitialBalance - amount
                    });
                });

            BalanceService.Setup(x => x.Check(It.IsAny<Decimal>()))
                .ReturnsAsync((Decimal amount) =>
                {
                    return ApiResult<Balance>.Success(
                    new Balance
                    {
                        CurrentBalance = InitialBalance
                    }
                );
                });

            EmptyBalanceService.Setup(x => x.Check(It.IsAny<Decimal>()))
                .ReturnsAsync((Decimal amount) =>
                {
                    return ApiResult<Balance>.BadRequest("Insufficient balance");
                });


            AlwaysWinGameStrategy.Setup(x => x.Play(It.IsAny<GameInputDTO>()))
                .Returns((GameInputDTO input) =>
                {
                    return new GameOutputDTO
                    {
                        GameWon = true,
                        BalanceChange = input.Stake * 9,
                        CurrentBalance = InitialBalance + input.Stake * 9
                    };
                });

            AlwaysLoseGameStrategy.Setup(x => x.Play(It.IsAny<GameInputDTO>()))
                .Returns((GameInputDTO input) =>
                {
                    return new GameOutputDTO
                    {
                        GameWon = false,
                        BalanceChange = -input.Stake,
                        CurrentBalance = InitialBalance -input.Stake
                    };
                });
        }

        public void Dispose()
        {
        }
    }
}