namespace src.Models;

public class Balance : AuditableEntity
{
    public string UserId {get; set; }
    public decimal Value { get; set; }

    public virtual ApplicationUser User { get; set; }
}
