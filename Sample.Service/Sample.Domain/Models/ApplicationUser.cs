using Microsoft.AspNetCore.Identity;

namespace Sample.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Balance Balance { get; set; }
    }
}