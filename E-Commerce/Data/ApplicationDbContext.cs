using E_Commerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products  { get; set; }
    }
}