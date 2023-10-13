using Microsoft.AspNetCore.Identity;

namespace src.Models;

public class ApplicationUser : IdentityUser
{
    public virtual Balance Balance { get; set; }
}
