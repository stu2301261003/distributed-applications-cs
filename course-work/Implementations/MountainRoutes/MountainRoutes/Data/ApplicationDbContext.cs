using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MountainRoutes.Models;

namespace MountainRoutes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mountain> Mountains { get; set; }
        public DbSet<MountainRoute> MountainRoutes { get; set; }
        public DbSet<Hut> Huts { get; set; }
    }
}
