namespace Sample.Business.DTO
{
    public class GameOutputDTO
    {
        public bool GameWon { get; set; }
        public int Number { get; set; }
        public int WinningNumber { get; set; }
        public decimal Stake { get; set; }
        public decimal BalanceChange { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}