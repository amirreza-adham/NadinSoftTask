using Microsoft.AspNetCore.Identity;

namespace NadinSoftTask.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
