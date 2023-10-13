using System.Collections.Generic;
using Sample.Extensions.Infrastrcture;

namespace Sample.DAL.Models
{
    public class Balance : AuditableEntity
    {
        public string UserId { get; set; }
        public decimal CurrentBalance { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
    }
}