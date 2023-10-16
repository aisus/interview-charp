using System.ComponentModel.DataAnnotations;

namespace Sample.Business.DTO
{
    public class GameInputDTO
    {
        [Range(1, 9)]
        public int Number { get; set; }
        [Range(0, int.MaxValue)]
        public decimal Stake { get; set; }
    }
}