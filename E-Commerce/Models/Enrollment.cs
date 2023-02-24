using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Models
{
    public class Enrollment
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
