using System;
using Sample.DAL.Enums;

namespace Sample.DAL.Models
{
    public class OperationOutputDTO
    {
        public OperationType Type { get; set; }
        public decimal BalanceChange { get; set; }
        public string Message { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}