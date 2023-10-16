using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Business.DTO
{
    public class BalanceInputDTO
    {
        
        [Range(0, int.MaxValue)]
        public decimal Amount { get; set; }
    }
}