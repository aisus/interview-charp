using System;
using Sample.Business.DTO;

namespace Sample.Business.Services.Game
{
    public interface IBetCalculator
    {
        GameOutputDTO CalculateBet(GameInputDTO input);
    }

    public class MatchingNumberOutOfTenBetCalculator : IBetCalculator
    {
        public GameOutputDTO CalculateBet(GameInputDTO input)
        {
            var randNumber = new Random().Next(10);
            return new GameOutputDTO
            {
                Stake = input.Stake,
                GameWon = input.Number == randNumber,
                Number = input.Number,
                WinningNumber = randNumber
            };
        }
    }
}