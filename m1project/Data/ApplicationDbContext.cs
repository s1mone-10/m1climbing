using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using m1project.Areas.Climbing.Models;

namespace m1project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<m1project.Areas.Climbing.Models.Crag> Crag { get; set; } = default!;
        public DbSet<m1project.Areas.Climbing.Models.Sector> Sector { get; set; } = default!;
        public DbSet<m1project.Areas.Climbing.Models.Route> Route { get; set; } = default!;
    }
}
