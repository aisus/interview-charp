using System;

namespace Sample.Business.DTO
{
    public class BalanceOutputDTO
    {
        public decimal CurrentBalance { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}