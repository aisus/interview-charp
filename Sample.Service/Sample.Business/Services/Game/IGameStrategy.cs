using Sample.Business.DTO;

namespace Sample.Business.Services.Game
{
    public interface IGameStrategy
    {
        public GameOutputDTO Play(GameInputDTO input);
    }

    public class DefaultGameStrategy : IGameStrategy
    {
        private readonly IBetCalculator betCalculator;
        private readonly IWinningsCalculator winningsCalculator;

        public DefaultGameStrategy()
        {
            betCalculator = new MatchingNumberOutOfTenBetCalculator();
            winningsCalculator = new NineTimesTheStakeWinningsCalculator();
        }

        public GameOutputDTO Play(GameInputDTO input)
        {
            var gameResult = betCalculator.CalculateBet(input);
            return winningsCalculator.CalculateAmount(gameResult);
        }
    }
}