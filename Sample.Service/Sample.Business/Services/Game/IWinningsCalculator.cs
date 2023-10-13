using Sample.Business.DTO;

namespace Sample.Business.Services.Game
{
    public interface IWinningsCalculator
    {
        GameOutputDTO CalculateAmount(GameOutputDTO result);
    }

    public class NineTimesTheStakeWinningsCalculator : IWinningsCalculator
    {
        public GameOutputDTO CalculateAmount(GameOutputDTO result)
        {
            if (result.GameWon)
            {
                result.BalanceChange = result.Stake * 9;
            }
            else 
            {
                result.BalanceChange = -result.Stake;
            }
            
            return result;
        }
    }
}