using FluentAssertions;
using Moq;
using Sample.Business.DTO;
using Sample.Business.Services;

namespace Sample.Service.Tests;

public class GameServiceTests : IClassFixture<BaseFixture>
{
    protected BaseFixture Fixture; 

    public GameServiceTests(BaseFixture fixture)
    {
        Fixture = fixture;
        Fixture.Reset();
    }

    [Fact]
    public async Task GameService_Should_UtilizeGameStrategy()
    {
        var sut = new GameService(Fixture.BalanceService.Object, Fixture.OperationsService.Object, Fixture.AlwaysWinGameStrategy.Object);

        var result = await sut.Play(new GameInputDTO { Number = 3, Stake = 1000 });

        result.IsSuccess.Should().BeTrue();
        result.Entity.CurrentBalance.Should().BeGreaterThan(BaseFixture.InitialBalance);
    }

    [Fact]
    public async Task GameService_Should_ReturnError_When_BalanceIsInsufficient()
    {
        var sut = new GameService(Fixture.EmptyBalanceService.Object, Fixture.OperationsService.Object, Fixture.AlwaysWinGameStrategy.Object);

        var result = await sut.Play(new GameInputDTO { Number = 3, Stake = 1000 });

        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Insufficient balance");
    }

    [Fact]
    public async Task GameService_Should_AddPointsToTheBalance_When_GameIsWon()
    {
        var sut = new GameService(Fixture.BalanceService.Object, Fixture.OperationsService.Object, Fixture.AlwaysWinGameStrategy.Object);

        var result = await sut.Play(new GameInputDTO { Number = 3, Stake = 1000 });

        result.IsSuccess.Should().BeTrue();
        Fixture.BalanceService.Verify(x => x.Add(It.IsAny<Decimal>()), Times.Once);
    }

    [Fact]
    public async Task GameService_Should_TakePointsFromTheBalance_When_GameIsLost()
    {
        var sut = new GameService(Fixture.BalanceService.Object, Fixture.OperationsService.Object, Fixture.AlwaysLoseGameStrategy.Object);

        var result = await sut.Play(new GameInputDTO { Number = 3, Stake = 1000 });

        result.IsSuccess.Should().BeTrue();
        Fixture.BalanceService.Verify(x => x.Take(It.IsAny<Decimal>()), Times.Once);
    }
}