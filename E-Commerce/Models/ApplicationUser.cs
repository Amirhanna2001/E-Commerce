using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Adress { get; set; }
        public string FirstName { get; set; }
        public string MiddelName { get; set; }
        public string LastName { get; set; }
    }
}
