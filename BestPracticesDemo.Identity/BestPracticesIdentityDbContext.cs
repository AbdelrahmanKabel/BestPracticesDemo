using BestPracticesDemo.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BestPracticesDemo.Identity
{
    public class BestPracticesIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public BestPracticesIdentityDbContext(DbContextOptions<BestPracticesIdentityDbContext> options) : base(options)
        {
        }

    }
}
