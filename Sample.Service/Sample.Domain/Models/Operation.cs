using System;
using Sample.DAL.Enums;
using Sample.Extensions.Infrastrcture;

namespace Sample.DAL.Models
{
    public class Operation : AuditableEntity
    {
        public Guid BalanceId { get; set; }
        public OperationType Type { get; set; }
        public decimal BalanceChange { get; set; }
        public string Message { get; set; }

        public virtual Balance Balance { get; set; }
    }
}